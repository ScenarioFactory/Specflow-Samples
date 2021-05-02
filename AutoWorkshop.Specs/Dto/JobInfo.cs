namespace AutoWorkshop.Specs.Dto
{
    using System;

    public class JobInfo
    {
        public JobInfo(string registration, string description, DateTime date, decimal hours, uint mileage)
        {
            Registration = registration;
            Description = description;
            Date = date;
            Hours = hours;
            Mileage = mileage;
        }

        public string Registration { get; }

        public string Description { get; }

        public DateTime Date { get; }

        public decimal Hours { get; }

        public uint Mileage { get; }
    }
}