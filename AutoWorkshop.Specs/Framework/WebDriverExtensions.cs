namespace AutoWorkshop.Specs.Framework
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    public static class WebDriverExtensions
    {
        public static void ClickElementWhenClickable(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));

                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .Click()
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static IAlert GetAlertWhenPresent(this IWebDriver driver)
        {
            return driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public static string GetElementAttributeValueWhenVisible(this IWebDriver driver, By locator, string attribute)
        {
            string WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator).GetAttribute(attribute);
            }

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static string GetElementTextWhenVisible(this IWebDriver driver, By locator)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Text;

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static ReadOnlyCollection<IWebElement> GetMultipleElementsWhenVisible(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            return driver.FindElements(locator);
        }

        public static string[] GetMultipleElementTextValuesWhenVisible(this IWebDriver driver, By locator)
        {
            string[] WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElements(locator).Select(e => e.Text).ToArray();
            }

            return FunctionRetrier.RetryOnException<string[], StaleElementReferenceException>(WebDriverActions);
        }

        public static string GetSelectedOptionTextWhenVisible(this IWebDriver driver, By locator)
        {
            string WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return new SelectElement(driver.FindElement(locator)).SelectedOption.Text;
            }

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }
        
        public static void SelectOptionByTextWhenVisible(this IWebDriver driver, By locator, string text)
        {
            IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }

        public static void SendKeysWhenVisible(this IWebDriver driver, By locator, string value)
        {
            void WebDriverActions()
            {
                IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                element.Clear();
                element.SendKeys(value);
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        private static WebDriverWait Wait(this IWebDriver driver, int waitSeconds = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
        }
    }
}
