namespace AutoWorkshop.DomainModel
{
    using System;

    public class VatRate
    {
        public VatRate(DateTime applicableFrom, decimal rate)
        {
            ApplicableFrom = applicableFrom;
            Rate = rate;
        }

        public DateTime ApplicableFrom { get; }

        public decimal Rate { get; }
    }
}
