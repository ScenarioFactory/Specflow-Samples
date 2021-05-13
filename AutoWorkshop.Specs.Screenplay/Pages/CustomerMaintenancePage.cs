namespace AutoWorkshop.Specs.Screenplay.Pages
{
    using OpenQA.Selenium;

    public static class CustomerMaintenancePage
    {
        public static readonly string Path = "custmaint.php";
        public static readonly By Title = By.Name("title");
        public static readonly By Name = By.Name("name");
        public static readonly By AddressLine1 = By.Name("address1");
        public static readonly By AddressLine2 = By.Name("address2");
        public static readonly By AddressLine3 = By.Name("address3");
        public static readonly By Postcode = By.Name("postcode");
        public static readonly By HomePhone = By.Name("homephone");
        public static readonly By Mobile = By.Name("mobile");
        public static readonly By Save = By.Name("save");
        public static readonly By AsYouTypeSearchResults = By.XPath("//div[@id='matchingPanel']//a");
    }
}
