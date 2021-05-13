namespace AutoWorkshop.Specs.Screenplay.WebDriver.Questions
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Dto;
    using OpenQA.Selenium;
    using Pattern;

    public class ToolbarButtons : WebQuestion<ToolbarButtonInfo[]>
    {
        private ToolbarButtons()
        {
        }

        public static ToolbarButtons WhichAreVisible()
        {
            return new ToolbarButtons();
        }

        public override ToolbarButtonInfo[] AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            ReadOnlyCollection<IWebElement> anchors = driver.WaitForElements(By.XPath("//fieldset//a"));

            return anchors
                .Select(anchor => new ToolbarButtonInfo(anchor))
                .ToArray();
        }
    }
}
