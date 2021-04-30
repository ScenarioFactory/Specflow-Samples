namespace AutoWorkshop.Specs.Dto
{
    using System;

    public class CarInfo
    {
        public CarInfo(string registration, uint customerId, string make, string model)
        {
            Registration = registration;
            CustomerId = customerId;
            Make = make;
            Model = model;
        }

        public string Registration { get; }

        public uint CustomerId { get; }

        public string Make { get; }

        public string Model { get; }
    }
}
