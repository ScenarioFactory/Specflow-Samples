namespace AutoWorkshop.Specs.Screenplay.Questions
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Drivers;
    using Dto;
    using OpenQA.Selenium;
    using Pattern;

    public class ToolbarButtons : WebQuestion<ToolbarButtonInfo[]>
    {
        private ToolbarButtons()
        {
        }

        public static ToolbarButtons CurrentlyVisible()
        {
            return new ToolbarButtons();
        }

        public override ToolbarButtonInfo[] AskAs(IActor actor, AutoWorkshopDriver driver)
        {
            ReadOnlyCollection<IWebElement> anchors = driver.FindElements(By.XPath("//fieldset//a"));

            return anchors
                .Select(anchor => new ToolbarButtonInfo(anchor))
                .ToArray();
        }
    }
}
