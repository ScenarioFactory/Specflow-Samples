namespace AutoWorkshop.Specs.UI
{
    using Framework;
    using OpenQA.Selenium;

    public class ChangeCarRegistrationPage
    {
        private const string PageUrl = "changereg.php";
        private static readonly By CurrentRegistration = By.Name("regis");
        private static readonly By NewRegistration = By.Name("newregis");
        private static readonly By UpdateRegistration = By.Name("change");
        private static readonly By SuccessMessage = By.XPath("//p[@class='largehead']");
        private static readonly By ErrorMessage = By.XPath("//p[@class='error']");

        private readonly AutoWorkshopDriver _driver;

        public ChangeCarRegistrationPage(AutoWorkshopDriver driver)
        {
            _driver = driver;
        }

        public void Open()
        {
            _driver.NavigateTo(PageUrl);
        }

        public void ChangeRegistration(string currentRegistration, string newRegistration)
        {
            _driver.SendKeysWhenVisible(CurrentRegistration, currentRegistration);
            _driver.SendKeysWhenVisible(NewRegistration, newRegistration);

            _driver.ClickElementWhenClickable(UpdateRegistration);

            _driver.SwitchTo().Alert().Accept();
        }

        public string GetSuccessMessage()
        {
            return _driver.GetElementTextWhenVisible(SuccessMessage);
        }

        public string GetErrorMessage()
        {
            return _driver.GetElementTextWhenVisible(ErrorMessage);
        }
    }
}
