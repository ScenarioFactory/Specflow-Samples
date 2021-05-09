namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Screenplay;

    public class Select : WebTask
    {
        private readonly By _locator;
        private readonly string _text;

        private Select(By locator, string text)
        {
            _locator = locator;
            _text = text;
        }

        public static Select ByText(By locator, string text) => new Select(locator, text);

        public override void PerformAs(Actor actor, AutoWorkshopDriver driver)
        {
            new SelectElement(driver.WaitForElement(_locator)).SelectByText(_text);
        }
    }
}
