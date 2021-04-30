namespace AutoWorkshop.Specs.UI
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    public class AutoWorkshopDriver : ChromeDriver
    {
        private const int TimeoutSeconds = 10;
        private static readonly string BaseUrl = Configuration.AppSettings["AutoWorkshop:Url"];
        private static readonly string AuthenticationCookieName = Configuration.AppSettings["AutoWorkshop:AuthenticationCookie:Name"];
        private static readonly string AuthenticationCookieValue = Configuration.AppSettings["AutoWorkshop:AuthenticationCookie:Value"];

        public AutoWorkshopDriver()
        {
            Url = BaseUrl;
            Manage().Cookies.AddCookie(new Cookie(AuthenticationCookieName, AuthenticationCookieValue));
        }

        public void NavigateTo(string path)
        {
            Url = Uri.EscapeUriString($"{BaseUrl}/{path}");
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
