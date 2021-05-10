namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using Pattern;

    public class Alert : WebTask
    {
        private Alert()
        {
        }

        public static Alert Accept()
        {
            return new Alert();
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.SwitchTo().Alert().Accept();
        }
    }
}
