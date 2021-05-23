namespace AutoWorkshop.Specs.Rest.Dto
{
    using System;

    public class CarInfo
    {
        public CarInfo()
        {
        }

        /// <summary>
        /// Constructor used by Dapper.
        /// </summary>
        public CarInfo(
            string registration,
            uint customerId,
            string make,
            string model,
            DateTime? motExpiry,
            byte suppressMotReminder)
        {
            Registration = registration;
            CustomerId = (int)customerId;
            Make = make;
            Model = model;
            MotExpiry = motExpiry;
            SuppressMotReminder = suppressMotReminder == 1;
        }

        public string Registration { get; set; }

        public int CustomerId { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTime? MotExpiry { get; set; }

        public bool SuppressMotReminder { get; set; }
    }
}
