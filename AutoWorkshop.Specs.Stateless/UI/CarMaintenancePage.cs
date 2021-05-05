namespace AutoWorkshop.Specs.Stateless.UI
{
    using Dto;
    using OpenQA.Selenium;

    public class CarMaintenancePage : Page
    {
        private const string PageUrl = "carmaint.php";
        private static readonly By Registration = By.Name("regis");
        private static readonly By Make = By.Name("make");
        private static readonly By Model = By.Name("model");
        private static readonly By Year = By.Name("year");
        private static readonly By CustomerId = By.Name("custid");
        private static readonly By Save = By.Name("save");

        public CarMaintenancePage(AutoWorkshopDriver driver) : base(driver)
        {
        }

        public void Open()
        {
            Driver.NavigateTo(PageUrl);
        }

        public void CreateCar(CarUiViewInfo viewInfo)
        {
            Driver.WaitForElement(Registration).SendKeys(viewInfo.Registration);

            var customerPicker = new CustomerPicker(Driver, Driver.FindElement(CustomerId));
            customerPicker.SetValue(viewInfo.CustomerId);

            Driver.FindElement(Make).SendKeys(viewInfo.Make);
            Driver.FindElement(Model).SendKeys(viewInfo.Model);
            Driver.FindElement(Year).SendKeys(viewInfo.Year);

            Driver.FindElement(Save).Click();
        }
    }
}
