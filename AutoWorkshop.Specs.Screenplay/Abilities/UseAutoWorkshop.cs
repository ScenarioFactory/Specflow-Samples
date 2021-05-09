namespace AutoWorkshop.Specs.Screenplay.Abilities
{
    using Drivers;
    using Screenplay;

    public class UseAutoWorkshop : IAbility
    {
        private UseAutoWorkshop(AutoWorkshopDriver driver)
        {
            Driver = driver;
        }

        public AutoWorkshopDriver Driver { get; }

        public static UseAutoWorkshop With(AutoWorkshopDriver driver) => new UseAutoWorkshop(driver);
    }
}
