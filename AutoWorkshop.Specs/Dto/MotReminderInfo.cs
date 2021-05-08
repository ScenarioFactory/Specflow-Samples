namespace AutoWorkshop.Specs.Dto
{
    using System;
    using Extensions;

    public class MotReminderInfo
    {
        /// <summary>
        /// MySql type-specific constructor used by Dapper.
        /// </summary>
        public MotReminderInfo(
            string registration,
            string motExpiry,
            string make,
            string model,
            string title,
            string name,
            string addressLine1,
            string addressLine2,
            string addressLine3,
            string postcode)
        {
            Registration = registration;
            MotExpiry = motExpiry.FromMySqlDate().Value;
            Make = make;
            Model = model;
            Title = title;
            Name = name;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            Postcode = postcode;
        }

        public string Registration { get; }

        public DateTime MotExpiry { get; }

        public string Make { get; }

        public string Model { get; }

        public string Title { get; }

        public string Name { get; }

        public string AddressLine1 { get; }

        public string AddressLine2 { get; }

        public string AddressLine3 { get; }

        public string Postcode { get; }
    }
}
