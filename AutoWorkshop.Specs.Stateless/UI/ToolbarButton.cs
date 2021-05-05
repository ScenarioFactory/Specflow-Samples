namespace AutoWorkshop.Specs.Stateless.UI
{
    using OpenQA.Selenium;

    public class ToolbarButton
    {
        private readonly IWebElement _anchor;

        public ToolbarButton(IWebElement anchor)
        {
            _anchor = anchor;
        }

        public string AltText => _anchor.FindElement(By.TagName("img")).GetAttribute("Alt");

        public void Click() => _anchor.Click();
    }
}