namespace AutoWorkshop.Specs.UI
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    public class AutoWorkshopDriver : ChromeDriver
    {
        private readonly AppSettings _appSettings;
        private const int TimeoutSeconds = 10;

        public AutoWorkshopDriver(AppSettings appSettings)
        {
            _appSettings = appSettings;

            Url = _appSettings.BaseUrl;
            Manage().Cookies.AddCookie(new Cookie(_appSettings.AuthenticationCookieName, _appSettings.AuthenticationCookieValue));
            Url = _appSettings.BaseUrl;
        }

        public void NavigateTo(string path)
        {
            Url = Uri.EscapeUriString($"{_appSettings.BaseUrl}/{path}");
        }

        public IWebElement WaitForElement(By findBy)
        {
            return Wait().Until(d =>
            {
                IWebElement element = d.FindElement(findBy);
                return element is {Displayed: true} ? element : null;
            });
        }

        public ReadOnlyCollection<IWebElement> WaitForElements(By findBy)
        {
            return Wait().Until(d =>
            {
                ReadOnlyCollection<IWebElement> elements = d.FindElements(findBy);
                return elements.Count > 0 && elements.All(e => e.Displayed) ? elements : null;
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
