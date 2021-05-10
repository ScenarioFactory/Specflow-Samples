namespace AutoWorkshop.Specs.Screenplay.Dto
{
    using OpenQA.Selenium;

    public class ToolbarButtonInfo
    {
        private readonly IWebElement _anchor;

        public ToolbarButtonInfo(IWebElement anchor)
        {
            _anchor = anchor;
        }

        public string AltText => _anchor.FindElement(By.TagName("img")).GetAttribute("Alt");
    }
}