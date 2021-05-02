namespace AutoWorkshop.Specs.Dto
{
    public class CarUiViewInfo
    {
        public CarUiViewInfo(string registration, string make, string model, string year)
        {
            Registration = registration;
            Make = make;
            Model = model;
            Year = year;
        }

        public string Registration { get; }

        public string Make { get; }

        public string Model { get; }

        public string Year { get; set; }
    }
}
