namespace AutoWorkshop.Specs.Screenplay.Pages
{
    using OpenQA.Selenium;

    public static class JobMaintenancePage
    {
        public static readonly By Description = By.Name("description");
        public static readonly By Start = By.Name("start");
        public static readonly By Hours = By.Name("hours");
        public static readonly By Mileage = By.Name("mileage");
        public static readonly By Save = By.Name("save");
    }
}
