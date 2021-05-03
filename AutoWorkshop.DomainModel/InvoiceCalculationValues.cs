namespace AutoWorkshop.DomainModel
{
    using System.Collections.Generic;

    public class InvoiceCalculationValues
    {
        public InvoiceCalculationValues(IReadOnlyCollection<VatRate> vatRates, IReadOnlyCollection<LabourRate> labourRates, IReadOnlyCollection<MotFee> motFees)
        {
            VatRates = vatRates;
            LabourRates = labourRates;
            MotFees = motFees;
        }

        public IReadOnlyCollection<VatRate> VatRates { get; set; }

        public IReadOnlyCollection<LabourRate> LabourRates { get; set; }

        public IReadOnlyCollection<MotFee> MotFees { get; set; }
    }
}
