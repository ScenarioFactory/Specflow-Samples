namespace AutoWorkshop.Specs.Stateless.UI
{
    using System.Linq;
    using OpenQA.Selenium;

    public class Toolbar
    {
        private readonly AutoWorkshopDriver _driver;

        public Toolbar(AutoWorkshopDriver driver)
        {
            _driver = driver;
        }

        public ToolbarButton[] Buttons
        {
            get
            {
                var anchors = _driver.FindElements(By.XPath("//fieldset//a"));

                return anchors
                    .Select(anchor => new ToolbarButton(anchor.GetAttribute("href"), anchor.FindElement(By.TagName("img")).GetAttribute("Alt")))
                    .ToArray();
            }
        }

        public bool ContainsLink(string altText)
        {
            return Buttons.Any(l => l.AltText == altText);
        }

        public void Click(string altText)
        {
            var anchors = _driver.FindElements(By.XPath("//fieldset//a"));

            IWebElement toolbarButtonToClick = anchors
                .Single(a => a.FindElement(By.TagName("img")).GetAttribute("Alt").Contains(altText));

            toolbarButtonToClick.Click();
        }
    }
}
