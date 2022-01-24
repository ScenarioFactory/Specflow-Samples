namespace AutoWorkshop.Specs.UI
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class AutoWorkshopDriver : ChromeDriver
    {
        private readonly AppSettings _appSettings;

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
    }
}
