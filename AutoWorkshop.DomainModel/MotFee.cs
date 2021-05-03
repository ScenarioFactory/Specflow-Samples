namespace AutoWorkshop.DomainModel
{
    using System;

    public class MotFee
    {
        public MotFee(DateTime applicableFrom, decimal fee)
        {
            ApplicableFrom = applicableFrom;
            Fee = fee;
        }

        public DateTime ApplicableFrom { get; }

        public decimal Fee { get; }
    }
}
