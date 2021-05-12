namespace AutoWorkshop.Specs.Screenplay.Abilities
{
    using Pattern;
    using WebDriver;

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
