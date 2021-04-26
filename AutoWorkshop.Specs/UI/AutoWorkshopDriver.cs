namespace AutoWorkshop.Specs.UI
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    public class AutoWorkshopDriver : ChromeDriver
    {
        private const int TimeoutSeconds = 30;
        private const string BaseUri = "http://localhost:8080/elitenet";
        private const string AuthenticationCookieName = "auth";
        private const string AuthenticationCookieValue = "authOK";

        private AutoWorkshopDriver()
        {
        }

        public static AutoWorkshopDriver CreateAuthenticatedInstance()
        {
            var driver = new AutoWorkshopDriver
            {
                Url = BaseUri
            };

            driver.Manage().Cookies.AddCookie(new Cookie(AuthenticationCookieName, AuthenticationCookieValue));

            return driver;
        }

        public void NavigateTo(string path)
        {
            Url = Uri.EscapeUriString($"{BaseUri}/{path}");
        }

        public IWebElement WaitForElement(By findBy)
        {
            return Wait().Until(d =>
            {
                IWebElement element = d.FindElement(findBy);
                return element is { Displayed: true } ? element : null;
            });
        }

        private WebDriverWait Wait()
        {
            var wait = new WebDriverWait(this, TimeSpan.FromSeconds(TimeoutSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait;
        }
    }
}
