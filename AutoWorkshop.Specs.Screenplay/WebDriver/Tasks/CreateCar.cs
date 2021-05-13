namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using Pages;
    using Pattern;

    public class CreateCar : WebTask
    {
        private readonly string _registration;
        private string _make;
        private string _model;
        private string _year;

        private CreateCar(string registration)
        {
            _registration = registration;
        }

        public static CreateCar WithRegistration(string registration)
        {
            return new CreateCar(registration);
        }

        public CreateCar AndMake(string make)
        {
            _make = make;
            return this;
        }

        public CreateCar AndModel(string model)
        {
            _model = model;
            return this;
        }

        public CreateCar ForYear(string year)
        {
            _year = year;
            return this;
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            actor.AttemptsTo(
                SendKeys.To(CarMaintenancePage.Registration, _registration),
                SendKeys.To(CarMaintenancePage.Make, _make),
                SendKeys.To(CarMaintenancePage.Model, _model),
                SendKeys.To(CarMaintenancePage.Year, _year),
                Click.On(CarMaintenancePage.Save));
        }
    }
}
