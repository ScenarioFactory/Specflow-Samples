namespace AutoWorkshop.Specs.UI
{
    using System;
    using OpenQA.Selenium;

    public class CalendarDatePicker
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement _webElement;

        public CalendarDatePicker(IWebDriver driver, IWebElement webElement)
        {
            _driver = driver;
            _webElement = webElement;
        }

        public void SetValue(DateTime value)
        {
            ((IJavaScriptExecutor) _driver).ExecuteScript("arguments[0].value = arguments[1]",
                _webElement, value.ToString("dd MMMM yyyy"));
        }
    }
}
