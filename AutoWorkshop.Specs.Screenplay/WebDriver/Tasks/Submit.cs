namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using OpenQA.Selenium;
    using Pattern;

    public class Submit : WebTask
    {
        private readonly By _locator;

        private Submit(By locator)
        {
            _locator = locator;
        }

        public static Submit On(By locator)
        {
            return new Submit(locator);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            driver.WaitForElement(_locator).Click();
        }
    }
}
