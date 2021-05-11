namespace AutoWorkshop.Specs.Screenplay.Abilities
{
    using Pattern;
    using WebDriver;

    public class UseAutoWorkshop : IAbility
    {
        private UseAutoWorkshop(AutoWorkshopDriver driver)
        {
            Driver = driver;
        }

        public AutoWorkshopDriver Driver { get; }

        public static UseAutoWorkshop With(AutoWorkshopDriver driver)
        {
            return new UseAutoWorkshop(driver);
        }
    }
}
