namespace AutoWorkshop.Specs.Screenplay.WebDriver.Questions
{
    using OpenQA.Selenium;
    using Pattern;

    public class Value : WebQuestion<string>
    {
        private readonly By _locator;

        private Value(By locator)
        {
            _locator = locator;
        }

        public static Value Of(By locator)
        {
            return new Value(locator);
        }

        protected override string AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            return driver.WaitForElement(_locator).GetAttribute("value");
        }
    }
}
