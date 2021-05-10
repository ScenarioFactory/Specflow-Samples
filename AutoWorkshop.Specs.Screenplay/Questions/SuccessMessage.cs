namespace AutoWorkshop.Specs.Screenplay.Questions
{
    using Drivers;
    using OpenQA.Selenium;
    using Pattern;

    public class SuccessMessage : WebQuestion<string>
    {
        private readonly By _locator;

        private SuccessMessage(By locator)
        {
            _locator = locator;
        }

        public static SuccessMessage For(By locator)
        {
            return new SuccessMessage(locator);
        }

        public override string AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            return driver.WaitForElement(_locator).Text;
        }
    }
}
