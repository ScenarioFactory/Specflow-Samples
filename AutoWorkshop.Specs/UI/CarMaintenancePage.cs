namespace AutoWorkshop.Specs.UI
{
    using System.Linq;
    using Dto;
    using OpenQA.Selenium;

    public class CarMaintenancePage : Page
    {
        private const string PageUrl = "carmaint.php";
        private static readonly By Registration = By.Name("regis");
        private static readonly By Make = By.Name("make");
        private static readonly By Model = By.Name("model");
        private static readonly By Year = By.Name("year");
        private static readonly By Save = By.Name("save");

        public CarMaintenancePage(AutoWorkshopDriver driver) : base(driver)
        {
        }

        public void CreateCar(CarUiViewInfo viewInfo)
        {
            Driver.WaitForElement(Registration).SendKeys(viewInfo.Registration);
            Driver.FindElement(Make).SendKeys(viewInfo.Make);
            Driver.FindElement(Model).SendKeys(viewInfo.Model);
            Driver.FindElement(Year).SendKeys(viewInfo.Year);

            Driver.FindElement(Save).Click();
        }

        public void AddNewJob()
        {
            Toolbar.Buttons.Single(b => b.Matches("Add a new job")).Click();
        }
    }
}
