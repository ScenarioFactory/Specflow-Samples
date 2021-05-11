namespace AutoWorkshop.Specs.Screenplay.Dto
{
    using System;

    public class JobUiViewInfo
    {
        public JobUiViewInfo(string registration, string description, DateTime date, decimal hours, int mileage)
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

        public int Mileage { get; }
    }
}