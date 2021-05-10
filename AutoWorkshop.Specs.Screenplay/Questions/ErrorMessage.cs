namespace AutoWorkshop.Specs.Screenplay.Questions
{
    using Drivers;
    using OpenQA.Selenium;
    using Pattern;

    public class ErrorMessage : WebQuestion<string>
    {
        private readonly By _locator;

        private ErrorMessage(By locator)
        {
            _locator = locator;
        }

        public static ErrorMessage For(By locator)
        {
            return new ErrorMessage(locator);
        }

        public override string AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            return driver.WaitForElement(_locator).Text;
        }
    }
}
