namespace AutoWorkshop.Specs.UI
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Framework;
    using OpenQA.Selenium;

    public class Toolbar
    {
        private readonly AutoWorkshopDriver _driver;

        public Toolbar(AutoWorkshopDriver driver)
        {
            _driver = driver;
        }

        public void ClickButtonByAltText(string startsWith)
        {
            void WebDriverActions()
            {
                ReadOnlyCollection<IWebElement> anchors = _driver.GetMultipleElementsWhenVisible(By.XPath("//fieldset//a"));

                anchors
                    .SingleOrDefault(anchor => anchor.FindElement(By.TagName("img")).GetAttribute("Alt").StartsWith(startsWith))
                    ?.Click();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public bool HasButtonWithAltText(string startsWith)
        {
            bool WebDriverActions()
            {
                ReadOnlyCollection<IWebElement> anchors = _driver.GetMultipleElementsWhenVisible(By.XPath("//fieldset//a"));

                return anchors
                    .Select(anchor => anchor.FindElement(By.TagName("img")).GetAttribute("Alt"))
                    .Any(altText => altText.StartsWith(startsWith));
            }

            return FunctionRetrier.RetryOnException<bool, StaleElementReferenceException>(WebDriverActions);
        }
    }
}
