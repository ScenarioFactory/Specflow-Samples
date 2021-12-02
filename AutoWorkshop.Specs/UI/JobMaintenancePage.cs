namespace AutoWorkshop.Specs.UI
{
    using Dto;
    using OpenQA.Selenium;

    public class JobMaintenancePage : Page
    {
        private static readonly By Description = By.Name("description");
        private static readonly By Start = By.Name("start");
        private static readonly By Hours = By.Name("hours");
        private static readonly By Mileage = By.Name("mileage");
        private static readonly By Save = By.Name("save");

        public JobMaintenancePage(AutoWorkshopDriver driver) : base(driver)
        {
        }

        public void CreateJob(JobUiViewInfo viewInfo)
        {
            Driver.WaitForElement(Description).SendKeys(viewInfo.Description);

            var datePicker = new CalendarDatePicker(Driver, Driver.FindElement(Start));
            datePicker.SetValue(viewInfo.Date);
            
            Driver.FindElement(Hours).SendKeys(viewInfo.Hours.ToString("0.##"));
            Driver.FindElement(Mileage).SendKeys(viewInfo.Mileage.ToString());

            Driver.FindElement(Save).Click();

            IAlert alert = Driver.Wait(5).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            if (alert != null && alert.Text.StartsWith("Have you checked MOT"))
            {
                alert.Accept();
            }
        }
    }
}
