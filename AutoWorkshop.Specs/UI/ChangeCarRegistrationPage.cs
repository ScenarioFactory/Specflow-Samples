namespace AutoWorkshop.Specs.UI
{
    using OpenQA.Selenium;

    public class ChangeCarRegistrationPage : Page
    {
        private const string PageUrl = "changereg.php";
        private static readonly By CurrentRegistration = By.Name("regis");
        private static readonly By NewRegistration = By.Name("newregis");
        private static readonly By UpdateRegistration = By.Name("change");
        private static readonly By SuccessMessage = By.XPath("//p[@class='largehead']");
        private static readonly By ErrorMessage = By.XPath("//p[@class='error']");

        public ChangeCarRegistrationPage(AutoWorkshopDriver driver) : base(driver)
        {
            Driver.NavigateTo(PageUrl);
        }

        public void ChangeRegistration(string currentRegistration, string newRegistration)
        {
            Driver.WaitForElement(CurrentRegistration).SendKeys(currentRegistration);
            Driver.FindElement(NewRegistration).SendKeys(newRegistration);

            Driver.FindElement(UpdateRegistration).Click();

            Driver.SwitchTo().Alert().Accept();
        }

        public string GetSuccessMessage()
        {
            return Driver.WaitForElement(SuccessMessage).Text;
        }

        public string GetErrorMessage()
        {
            return Driver.WaitForElement(ErrorMessage).Text;
        }
    }
}
