namespace AutoWorkshop.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class Job
    {
        public Job(int jobId, int customerId, int vehicleId, string description, DateTime jobDate, bool isMotTest)
        {
            JobId = jobId;
            CustomerId = customerId;
            VehicleId = vehicleId;
            Description = description;
            JobDate = jobDate;
            IsMotTest = isMotTest;
        }

        public int JobId { get; }

        public int CustomerId { get; }

        public int VehicleId { get; }

        public string Description { get; }

        public DateTime JobDate { get; }

        public bool IsMotTest { get; }

        public decimal LabourHours { get; set; }

        public List<Part> Parts { get; } = new List<Part>();

        public InvoiceCosts CalculateInvoiceCosts(InvoiceCalculationValues invoiceCalculationValues)
        {
            var applicableVatRate = GetApplicableVatRate(invoiceCalculationValues);
            var applicableLabourRate = GetApplicableLabourRate(invoiceCalculationValues);
            var applicableMotFee = GetApplicableMotFee(invoiceCalculationValues);

            applicableVatRate.ShouldNotBeNull($"No historic VAT rate available for {JobDate}");
            applicableLabourRate.ShouldNotBeNull($"No historic Labour rate available for {JobDate}");
            applicableMotFee.ShouldNotBeNull($"No historic MOT fee available for {JobDate}");

            var partsCost = Parts.Sum(p => p.Cost);
            var labourCost = LabourHours * applicableLabourRate.Rate;
            var vatCost = (partsCost + labourCost) / 100 * applicableVatRate.Rate;
            var motCost = IsMotTest ? applicableMotFee.Fee : 0;
            var totalCost = partsCost + labourCost + vatCost + motCost;

            return new InvoiceCosts(labourCost, partsCost, vatCost, motCost, totalCost);
        }

        private VatRate GetApplicableVatRate(InvoiceCalculationValues invoiceCalculationValues)
        {
            return invoiceCalculationValues.VatRates
                .Where(vr => JobDate >= vr.ApplicableFrom)
                .OrderByDescending(vr => vr.ApplicableFrom)
                .FirstOrDefault();
        }

        private LabourRate GetApplicableLabourRate(InvoiceCalculationValues invoiceCalculationValues)
        {
            return invoiceCalculationValues.LabourRates
                .Where(lr => JobDate >= lr.ApplicableFrom)
                .OrderByDescending(lr => lr.ApplicableFrom)
                .FirstOrDefault();
        }

        private MotFee GetApplicableMotFee(InvoiceCalculationValues invoiceCalculationValues)
        {
            return invoiceCalculationValues.MotFees
                .Where(mf => JobDate >= mf.ApplicableFrom)
                .OrderByDescending(mf => mf.ApplicableFrom)
                .FirstOrDefault();
        }
    }
}
