namespace AutoWorkshop.Specs.UI
{
    using OpenQA.Selenium;

    public class ToolbarButton
    {
        private readonly IWebElement _anchor;

        public ToolbarButton(IWebElement anchor)
        {
            _anchor = anchor;
        }

        private string AltText => _anchor.FindElement(By.TagName("img")).GetAttribute("Alt");

        public bool Matches(string matchText) => AltText.Contains(matchText);

        public void Click() => _anchor.Click();
    }
}