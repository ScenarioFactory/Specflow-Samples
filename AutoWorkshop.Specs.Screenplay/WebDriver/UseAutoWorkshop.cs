namespace AutoWorkshop.Specs.Screenplay.WebDriver
{
    using Pattern;

    public class UseAutoWorkshop : IAbility
    {
        public AutoWorkshopDriver Driver { get; }

        private UseAutoWorkshop(AutoWorkshopDriver driver)
        {
            Driver = driver;
        }

        public static UseAutoWorkshop With(AutoWorkshopDriver driver)
        {
            return new UseAutoWorkshop(driver);
        }
    }
}
