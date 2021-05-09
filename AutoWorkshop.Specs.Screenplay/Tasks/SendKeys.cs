namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using OpenQA.Selenium;
    using Screenplay;

    public class SendKeys : WebTask
    {
        private readonly By _locator;
        private readonly string _keys;

        private SendKeys(By locator, string keys)
        {
            _locator = locator;
            _keys = keys;
        }

        public static SendKeys To(By locator, string keys) => new SendKeys(locator, keys);

        public override void PerformAs(Actor actor, AutoWorkshopDriver driver)
        {
            driver.WaitForElement(_locator).SendKeys(_keys);
        }
    }
}
