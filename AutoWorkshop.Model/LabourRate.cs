﻿namespace AutoWorkshop.Model
{
    using System;

    public class LabourRate
    {
        public LabourRate(DateTime applicableFrom, decimal rate)
        {
            ApplicableFrom = applicableFrom;
            Rate = rate;
        }

        public DateTime ApplicableFrom { get; }

        public decimal Rate { get; }
    }
}