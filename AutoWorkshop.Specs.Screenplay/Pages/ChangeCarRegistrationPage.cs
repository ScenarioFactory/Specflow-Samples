namespace AutoWorkshop.Specs.Screenplay.Pages
{
    using OpenQA.Selenium;

    public static class ChangeCarRegistrationPage
    {
        public static readonly string Path = "changereg.php";
        public static readonly By CurrentRegistration = By.Name("regis");
        public static readonly By NewRegistration = By.Name("newregis");
        public static readonly By UpdateRegistration = By.Name("change");
        public static readonly By SuccessMessage = By.XPath("//p[@class='largehead']");
        public static readonly By ErrorMessage = By.XPath("//p[@class='error']");
    }
}
