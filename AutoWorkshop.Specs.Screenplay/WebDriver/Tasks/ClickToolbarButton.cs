namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using OpenQA.Selenium;
    using Pattern;

    public class ClickToolbarButton : WebTask
    {
        private readonly string _altTextStartsWith;

        private ClickToolbarButton(string altTextStartsWith)
        {
            _altTextStartsWith = altTextStartsWith;
        }

        public static ClickToolbarButton WithAltText(string altTextStartsWith)
        {
            return new ClickToolbarButton(altTextStartsWith);
        }

        public override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            ReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.XPath("//fieldset//a"));

            var anchor = anchors
                .Single(a => a.FindElement(By.TagName("img")).GetAttribute("Alt").StartsWith(_altTextStartsWith));

            anchor.Click();
        }
    }
}
