namespace AutoWorkshop.Specs.Screenplay.Questions
{
    using Drivers;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Pattern;

    public class SelectedOptionText : WebQuestion<string>
    {
        private readonly By _locator;

        private SelectedOptionText(By locator)
        {
            _locator = locator;
        }

        public static SelectedOptionText Of(By locator)
        {
            return new SelectedOptionText(locator);
        }

        public override string AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            return new SelectElement(driver.WaitForElement(_locator)).SelectedOption.Text;
        }
    }
}