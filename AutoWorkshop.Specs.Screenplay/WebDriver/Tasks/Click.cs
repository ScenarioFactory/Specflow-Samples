namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using OpenQA.Selenium;
    using Pattern;

    public class Click : WebTask
    {
        private readonly By _locator;

        private Click(By locator)
        {
            _locator = locator;
        }

        public static Click On(By locator)
        {
            return new Click(locator);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.WaitForElement(_locator).Click();
        }
    }
}
