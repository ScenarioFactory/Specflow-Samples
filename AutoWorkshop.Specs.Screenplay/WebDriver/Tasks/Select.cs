namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Pattern;

    public class Select : WebTask
    {
        private readonly By _locator;
        private readonly string _text;

        private Select(By locator, string text)
        {
            _locator = locator;
            _text = text;
        }

        public static Select ByText(By locator, string text)
        {
            return new Select(locator, text);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            new SelectElement(driver.WaitForElement(_locator)).SelectByText(_text);
        }
    }
}
