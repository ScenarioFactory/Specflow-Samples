namespace Samples.AutomationFramework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    public static class WebDriverExtensions
    {
        public static void BringElementToTopAndClick(this IWebDriver driver, By locator)
        {
            IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));

            string script = @"
                arguments[0].setAttribute('style.zIndex', '-1');
                arguments[0].click();
            ";

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script, element);
        }

        public static void ClickElementAtOffsetWhenClickable(this IWebDriver driver, By locator, int x, int y)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));

                Size size = elementToClick.Size;

                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .MoveByOffset(size.Width / 2, size.Height / 2)
                    .MoveByOffset(x, y)
                    .Click()
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

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

        public static void ClickElementWhenExists(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));

                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .Click()
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void ClickFirstElementWhenVisible(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                elements.First().Click();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void ClickLastElementWhenVisible(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                elements.Last().Click();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void ClickNoElement(this IWebDriver driver)
        {
            driver.ClickElementWhenExists(By.XPath("//body"));
        }

        public static void ClickNthElementWhenVisible(this IWebDriver driver, By locator, int index)
        {
            void WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                elements[index].Click();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void CreateTextBox(this IWebDriver driver, string elementId)
        {
            string script = $@"
                var input = document.createElement('input');
                input.type = 'text';
                input.id = '{elementId}';
                document.body.appendChild(input);
                ";

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);
        }

        public static void DragAndDropElement(this IWebDriver driver, By elementToDragLocator, By elementToDropOntoLocator)
        {
            void WebDriverActions()
            {
                IWebElement elementToDrag = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementToDragLocator));
                IWebElement elementToDropOnto = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementToDropOntoLocator));

                new Actions(driver)
                    .ClickAndHold(elementToDrag)
                    .MoveToElement(elementToDropOnto)
                    .Release(elementToDrag)
                    .Build()
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static int GetDisplayedElementCount(this IWebDriver driver, By locator)
        {
            int WebDriverActions()
            {
                try
                {
                    return driver.FindElements(locator).Count(e => e.Displayed);
                }
                catch (NoSuchElementException)
                {
                    return 0;
                }
            }

            return FunctionRetrier.RetryOnException<int, StaleElementReferenceException>(WebDriverActions);
        }

        public static string GetElementAttributeValueWhenVisible(this IWebDriver driver, By locator, string attribute)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).GetAttribute(attribute);

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static Point GetElementLocationWhenVisible(this IWebDriver driver, By locator)
        {
            Point WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Location;

            return FunctionRetrier.RetryOnException<Point, StaleElementReferenceException>(WebDriverActions);
        }

        public static string[] GetElementsTextIfExists(this IWebDriver driver, By locator)
        {
            string[] WebDriverActions()
            {
                driver.Wait(1).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                return driver.FindElements(locator).Select(e => e.Text.Trim()).ToArray();
            }

            try
            {
                return FunctionRetrier.RetryOnException<string[], StaleElementReferenceException>(WebDriverActions, 1);
            }
            catch (WebDriverTimeoutException wde) when (wde.InnerException is NoSuchElementException)
            {
                return Array.Empty<string>();
            }
        }

        public static string[] GetElementsTextWhenVisible(this IWebDriver driver, By locator)
        {
            string[] WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElements(locator).Select(e => e.Text.Trim()).ToArray();
            }

            return FunctionRetrier.RetryOnException<string[], StaleElementReferenceException>(WebDriverActions);
        }

        public static IList<IWebElement> GetElementsWhenExists(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            return driver.FindElements(locator);
        }

        public static IList<IWebElement> GetElementsWhenVisible(this IWebDriver driver, By locator)
        {
            IWebElement[] foundVisibleElements = Array.Empty<IWebElement>();

            IWebElement[] WebDriverActions() => driver.FindElements(locator).Where(e => e.Displayed).ToArray();

            Poller.PollForSuccess(() =>
            {
                foundVisibleElements = FunctionRetrier.RetryOnException<IWebElement[], StaleElementReferenceException>(WebDriverActions);
                return foundVisibleElements.Any();
            });

            if (foundVisibleElements.None())
            {
                throw new Exception($"No visible elements found for locator {locator}");
            }

            return foundVisibleElements;
        }

        public static string GetElementTextWhenExists(this IWebDriver driver, By locator)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Text;

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static string GetElementTextWhenVisible(this IWebDriver driver, By locator)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Text;

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static IWebElement GetElementWhenExists(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        public static IWebElement GetElementWhenVisible(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static string GetTextBoxValueWhenVisible(this IWebDriver driver, By locator)
        {
            return driver.GetElementAttributeValueWhenVisible(locator, "value");
        }

        // this method deliberately does not wait for the element to become visible,
        // but returns an (almost) immediate result with 1 second tolerance
        public static bool IsElementDisplayed(this IWebDriver driver, By locator)
        {
            TimeSpan currentImplicitWait = driver.Manage().Timeouts().ImplicitWait;

            bool isElementDisplayed;

            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

                // not wrapped in StaleElementReferenceException handling to assist local debugging
                isElementDisplayed = driver.Wait(1).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch (Exception)
            {
                isElementDisplayed = false;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = currentImplicitWait;
            }

            return isElementDisplayed;
        }

        public static bool IsElementDisplayedWhenVisible(this IWebDriver driver, By locator)
        {
            bool isElementDisplayed;

            try
            {
                // not wrapped in StaleElementReferenceException handling to assist local debugging
                isElementDisplayed = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch (Exception)
            {
                isElementDisplayed = false;
            }

            return isElementDisplayed;
        }

        public static bool IsElementEnabledWhenVisible(this IWebDriver driver, By locator)
        {
            bool WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Enabled;

            return FunctionRetrier.RetryOnException<bool, StaleElementReferenceException>(WebDriverActions);
        }

        public static bool IsElementSelected(this IWebDriver driver, By locator)
        {
            bool WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Selected;

            return FunctionRetrier.RetryOnException<bool, StaleElementReferenceException>(WebDriverActions);
        }

        public static void MoveToElement(this IWebDriver driver, By locator)
        {
            new Actions(driver)
                .MoveToElement(driver.GetElementWhenVisible(locator))
                .Perform();
        }

        public static void PasteIntoElementWhenExists(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));

                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .Click()
                    .KeyDown(Keys.Control)
                    .SendKeys("V")
                    .KeyUp(Keys.Control)
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void RefreshBrowser(this IWebDriver driver, TimeSpan? waitTime = null)
        {
            driver.Navigate().Refresh();

            if (waitTime.HasValue)
            {
                Task.Delay(waitTime.Value).Wait();
            }
        }

        public static void ScrollDownBy(this IWebDriver driver, int pixels)
        {
            string windowScroll = $"window.scrollBy(0,{pixels})";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(windowScroll, "");
        }

        public static void ScrollIntoView(this IWebDriver driver, By targetElementLocator)
        {
            IWebElement targetElement = driver.GetElementWhenExists(targetElementLocator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView()", targetElement);
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }
        public static void ScrollToRight(this IWebDriver driver, int pixels)
        {
            string windowScroll = $"window.scrollBy({pixels},0)";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(windowScroll, "");
        }
        public static void ScrollToRightWhenScrollBarExists(this IWebDriver driver, By horizontalScrollBarLocator, int pixels)
        {
            try
            {
                IWebElement scrollBar = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(horizontalScrollBarLocator));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript($"arguments[0].scrollLeft += {pixels}", scrollBar);
            }
            catch (Exception)
            {
                // no scrollbar present
            }
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }

        public static void SendKeysWhenVisible(this IWebDriver driver, By locator, string keys)
        {
            void WebDriverActions()
            {
                IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

                new Actions(driver)
                    .MoveToElement(element)
                    .Click()
                    .SendKeys(keys)
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void SetElementAttribute(this IWebDriver driver, IWebElement element, string name, string value)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", element, name, value);
        }

        public static void SetTextBoxValueWhenVisible(this IWebDriver driver, By locator, string value)
        {
            void WebDriverActions()
            {
                IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                element.Clear();
                element.SendKeys(value);
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void WaitForSpinner(this IWebDriver driver, By locator)
        {
            bool isSpinnerInvisible = Poller.PollForSuccess(() => !driver.IsElementDisplayed(locator));

            if (!isSpinnerInvisible)
            {
                throw new Exception("Spinner is still present after waiting");
            }
        }

        public static void WaitForSpinners(this IWebDriver driver, By locator)
        {
            bool AreAllSpinnersInvisible()
            {
                ReadOnlyCollection<IWebElement> spinnerElements = driver.FindElements(locator);

                return spinnerElements.None(e => e.Displayed);
            }

            bool allSpinnersAreInvisible = Poller.PollForSuccess(() => FunctionRetrier.RetryOnException<bool, StaleElementReferenceException>(AreAllSpinnersInvisible));

            if (!allSpinnersAreInvisible)
            {
                throw new Exception("Spinners are still present after waiting");
            }
        }

        public static void WaitUntilElementIsVisible(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitUntilElementIsClickable(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitUntilElementIsInvisible(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        private static WebDriverWait Wait(this IWebDriver driver, int waitSeconds = 10)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
        }
    }
}