namespace AutoWorkshop.Specs.Screenplay.Questions
{
    using Drivers;
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

        public override string AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            return driver.WaitForElement(_locator).GetAttribute("value");
        }
    }
}
