namespace AutoWorkshop.Specs.UI
{
    using Dto;
    using Framework;
    using OpenQA.Selenium;

    public class JobMaintenancePage
    {
        private static readonly By Description = By.Name("description");
        private static readonly By Start = By.Name("start");
        private static readonly By Hours = By.Name("hours");
        private static readonly By Mileage = By.Name("mileage");
        private static readonly By Save = By.Name("save");

        private readonly AutoWorkshopDriver _driver;

        public JobMaintenancePage(AutoWorkshopDriver driver)
        {
            _driver = driver;
        }

        public void CreateJob(JobUiViewInfo viewInfo)
        {
            _driver.SendKeysWhenVisible(Description, viewInfo.Description);

            CalendarDatePicker datePicker = new CalendarDatePicker(_driver, _driver.FindElement(Start));
            datePicker.SetValue(viewInfo.Date);

            _driver.SendKeysWhenVisible(Hours, viewInfo.Hours.ToString("0.##"));
            _driver.SendKeysWhenVisible(Mileage, viewInfo.Mileage.ToString());

            _driver.ClickElementWhenClickable(Save);

            IAlert alert = _driver.GetAlertWhenPresent();

            if (alert != null && alert.Text.StartsWith("Have you checked MOT"))
            {
                alert.Accept();
            }
        }
    }
}
