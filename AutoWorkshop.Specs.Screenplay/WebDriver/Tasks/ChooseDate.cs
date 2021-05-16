namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using System;
    using OpenQA.Selenium;
    using Pattern;

    public class ChooseDate : WebTask
    {
        private readonly By _locator;
        private readonly DateTime _date;

        private ChooseDate(By locator, DateTime date)
        {
            _locator = locator;
            _date = date.Date;
        }

        public static ChooseDate For(By locator, DateTime date)
        {
            return new ChooseDate(locator, date);
        }

        protected override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            IWebElement webElement = driver.WaitForElement(_locator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1]", webElement, _date.ToString("dd MMMM yyyy"));
        }
    }
}
