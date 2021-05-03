namespace AutoWorkshop.Specs.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;
    using Extensions;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class InvoicingSteps
    {
        private IReadOnlyCollection<VatRate> _vatRates;
        private IReadOnlyCollection<LabourRate> _labourRates;
        private IReadOnlyCollection<MotFee> _motFees;
        private Job _job;
        private InvoiceCosts _invoiceCosts;

        [Given(@"the current VAT rate is (.*)%")]
        public void GivenTheCurrentVatRateIs(decimal currentVatRate)
        {
            _vatRates = new[] { new VatRate(DateTime.MinValue, currentVatRate) };
        }

        [Given(@"the following historic VAT rates")]
        public void GivenTheFollowingVatRates(Table table)
        {
            _vatRates = table.Rows
                .Select(row => new VatRate(row.ParseDate("Applicable From"), row.ParseDecimal("Rate")))
                .ToList();
        }

        [Given(@"the current labour rate is £(.*) per hour")]
        public void GivenTheCurrentLabourRateIsPerHour(decimal currentLabourRate)
        {
            _labourRates = new[] { new LabourRate(DateTime.MinValue, currentLabourRate) };
        }

        [Given(@"the following historic labour rates")]
        public void GivenTheFollowingHistoricLabourRates(Table table)
        {
            _labourRates = table.Rows
                .Select(row => new LabourRate(row.ParseDate("Applicable From"), row.ParseDecimal("Rate")))
                .ToList();
        }

        [Given(@"the current MOT fee is £(.*)")]
        public void GivenTheCurrentMotFeeIs(decimal currentMotFee)
        {
            _motFees = new[] { new MotFee(DateTime.MinValue, currentMotFee) };
        }

        [Given(@"the following historic MOT fees")]
        public void GivenTheFollowingHistoricMotFees(Table table)
        {
            _motFees = table.Rows
                .Select(row => new MotFee(row.ParseDate("Applicable From"), row.ParseDecimal("Fee")))
                .ToList();
        }

        [Given(@"the following job")]
        public void GivenTheFollowingJob(Table table)
        {
            var values = table.Rows.Single();

            _job = new Job(
                1,
                1,
                1,
                "Test job",
                values.ParseDate("Job Date"),
                values.ParseBoolean("MOT Test"))
            {
                LabourHours = values.ParseDecimal("Labour Hours")
            };
        }

        [Given(@"the following parts")]
        public void GivenTheFollowingParts(Table table)
        {
            _job.Should().NotBeNull();

            table.Rows.ToList().ForEach(row =>
            {
                var part = new Part(1, row["Description"], row.ParseDecimal("Cost"));
                _job.Parts.Add(part);
            });
        }

        [When(@"the job invoice is calculated")]
        public void WhenTheJobInvoiceIsCalculated()
        {
            _job.Should().NotBeNull();
            _vatRates.Should().NotBeNull();
            _labourRates.Should().NotBeNull();
            _motFees.Should().NotBeNull();

            var invoiceCalculationValues = new InvoiceCalculationValues(_vatRates, _labourRates, _motFees);

            _invoiceCosts = _job.CalculateInvoiceCosts(invoiceCalculationValues);
        }

        [Then(@"the job should have the following invoiced costs")]
        public void ThenTheJobShouldHaveTheFollowingInvoicedCosts(Table table)
        {
            _invoiceCosts.Should().NotBeNull();

            var expectedValues = table.Rows.Single();

            _invoiceCosts.Parts.Should().Be(expectedValues.ParseDecimal("Parts"));
            _invoiceCosts.Labour.Should().Be(expectedValues.ParseDecimal("Labour"));
            _invoiceCosts.Vat.Should().Be(expectedValues.ParseDecimal("VAT"));
            _invoiceCosts.Mot.Should().Be(expectedValues.ParseDecimal("MOT"));
            _invoiceCosts.Total.Should().Be(expectedValues.ParseDecimal("Total"));
        }
    }
}
