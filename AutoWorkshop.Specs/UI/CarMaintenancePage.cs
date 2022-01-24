namespace AutoWorkshop.Specs.UI
{
    using Dto;
    using Framework;
    using OpenQA.Selenium;

    public class CarMaintenancePage
    {
        private static readonly By Registration = By.Name("regis");
        private static readonly By Make = By.Name("make");
        private static readonly By Model = By.Name("model");
        private static readonly By Year = By.Name("year");
        private static readonly By Save = By.Name("save");

        private readonly AutoWorkshopDriver _driver;
        private readonly Toolbar _toolbar;

        public CarMaintenancePage(AutoWorkshopDriver driver, Toolbar toolbar)
        {
            _driver = driver;
            _toolbar = toolbar;
        }

        public void CreateCar(CarUiViewInfo viewInfo)
        {
            _driver.SendKeysWhenVisible(Registration, viewInfo.Registration);
            _driver.SendKeysWhenVisible(Make, viewInfo.Make);
            _driver.SendKeysWhenVisible(Model, viewInfo.Model);
            _driver.SendKeysWhenVisible(Year, viewInfo.Year);

            _driver.ClickElementWhenClickable(Save);
        }

        public void AddNewJob()
        {
            _toolbar.ClickButtonByAltText("Add a new job");
        }
    }
}
