namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using OpenQA.Selenium;
    using Pattern;

    public class SendKeys : WebTask
    {
        private readonly By _locator;
        private readonly string _keys;

        private SendKeys(By locator, string keys)
        {
            _locator = locator;
            _keys = keys;
        }

        public static SendKeys To(By locator, string keys)
        {
            return new SendKeys(locator, keys);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.WaitForElement(_locator).SendKeys(_keys);
        }
    }
}
