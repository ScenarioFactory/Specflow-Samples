namespace AutoWorkshop.Specs.Screenplay.Dto
{
    using System;
    using Extensions;

    public class CarInfo
    {
        public CarInfo(string registration, int customerId, string make, string model, DateTime? motExpiry, bool suppressMotReminder)
        {
            Registration = registration;
            CustomerId = customerId;
            Make = make;
            Model = model;
            MotExpiry = motExpiry;
            SuppressMotReminder = suppressMotReminder;
        }

        /// <summary>
        /// MySql constructor used by Dapper.
        /// </summary>
        private CarInfo(string registration, uint customerId, string make, string model, string motExpiry, byte suppressMotReminder)
            : this(registration, (int)customerId, make, model, motExpiry.FromMySqlDate(), suppressMotReminder == 1)
        {
        }

        public string Registration { get; }

        public int CustomerId { get; }

        public string Make { get; }

        public string Model { get; }

        public DateTime? MotExpiry { get; }

        public bool SuppressMotReminder { get; }
    }
}
