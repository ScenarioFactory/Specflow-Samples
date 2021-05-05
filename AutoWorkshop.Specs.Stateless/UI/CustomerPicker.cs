namespace AutoWorkshop.Specs.Stateless.UI
{
    using OpenQA.Selenium;

    public class CustomerPicker
    {
        private readonly IWebDriver _driver;
        private readonly IWebElement _webElement;

        public CustomerPicker(IWebDriver driver, IWebElement webElement)
        {
            _driver = driver;
            _webElement = webElement;
        }

        public void SetValue(int value)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1]",
                _webElement, value.ToString());
        }
    }
}
