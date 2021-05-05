namespace AutoWorkshop.Specs.Stateless.Dto
{
    public class CarUiViewInfo
    {
        public CarUiViewInfo(string registration, int customerId, string make, string model, string year)
        {
            Registration = registration;
            CustomerId = customerId;
            Make = make;
            Model = model;
            Year = year;
        }

        public string Registration { get; }

        public int CustomerId { get; }

        public string Make { get; }

        public string Model { get; }

        public string Year { get; set; }
    }
}
