namespace AutoWorkshop.Specs.UI
{
    using System.Linq;
    using Dto;
    using OpenQA.Selenium;

    public class Toolbar
    {
        private readonly AutoWorkshopDriver _driver;

        public Toolbar(AutoWorkshopDriver driver)
        {
            _driver = driver;
        }

        public LinkInfo[] Links
        {
            get
            {
                var anchors = _driver.FindElements(By.XPath("//fieldset//a"));

                return anchors
                    .Select(anchor => new LinkInfo(anchor.GetAttribute("href"), anchor.FindElement(By.TagName("img")).GetAttribute("Alt")))
                    .ToArray();
            }
        }

        public bool ContainsLink(string altText)
        {
            return Links.Any(l => l.AltText == altText);
        }
    }
}
