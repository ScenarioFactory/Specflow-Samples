namespace AutoWorkshop.Specs.UI
{
    using Dto;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class CustomerMaintenancePage
    {
        private const string PageUrl = "custmaint.php";
        private static readonly By Title = By.Name("title");
        private static readonly By Name = By.Name("name");
        private static readonly By AddressLine1 = By.Name("address1");
        private static readonly By AddressLine2 = By.Name("address2");
        private static readonly By AddressLine3 = By.Name("address3");
        private static readonly By Postcode = By.Name("postcode");
        private static readonly By HomePhone = By.Name("homephone");
        private static readonly By Mobile = By.Name("mobile");
        private static readonly By Save = By.Name("save");

        private readonly AutoWorkshopDriver _driver;

        public CustomerMaintenancePage(AutoWorkshopDriver driver)
        {
            _driver = driver;
            _driver.NavigateTo(PageUrl);
        }

        public void CreateCustomer(CustomerUiInput uiInput)
        {
            var titleSelectElement = new SelectElement(_driver.WaitForElement(Title));
            titleSelectElement.SelectByText(uiInput.Title);

            _driver.FindElement(Name).SendKeys(uiInput.Name);
            _driver.FindElement(AddressLine1).SendKeys(uiInput.AddressLine1);
            _driver.FindElement(AddressLine2).SendKeys(uiInput.AddressLine2);
            _driver.FindElement(AddressLine3).SendKeys(uiInput.AddressLine3);
            _driver.FindElement(Postcode).SendKeys(uiInput.Postcode);
            _driver.FindElement(HomePhone).SendKeys(uiInput.HomePhone);
            _driver.FindElement(Mobile).SendKeys(uiInput.Mobile);

            _driver.FindElement(Save).Click();
        }
    }
}
