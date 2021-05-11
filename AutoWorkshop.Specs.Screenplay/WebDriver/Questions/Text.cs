namespace AutoWorkshop.Specs.Screenplay.WebDriver.Questions
{
    using OpenQA.Selenium;
    using Pattern;

    public class Text : WebQuestion<string>
    {
        private readonly By _locator;

        private Text(By locator)
        {
            _locator = locator;
        }

        public static Text Of(By locator)
        {
            return new Text(locator);
        }

        public override string AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            return driver.WaitForElement(_locator).Text;
        }
    }
}
