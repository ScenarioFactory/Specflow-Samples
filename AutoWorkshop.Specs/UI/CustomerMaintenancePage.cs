namespace AutoWorkshop.Specs.UI
{
    using System.Linq;
    using Dto;
    using Model;
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

        public CustomerMaintenancePage(AutoWorkshopDriver driver, int customerId)
        {
            _driver = driver;
            _driver.NavigateTo($"{PageUrl}?custid={customerId}");
        }

        public void CreateCustomer(CustomerUiViewInfo viewInfo)
        {
            var titleSelectElement = new SelectElement(_driver.WaitForElement(Title));
            titleSelectElement.SelectByText(viewInfo.Title);

            _driver.FindElement(Name).SendKeys(viewInfo.Name);
            _driver.FindElement(AddressLine1).SendKeys(viewInfo.AddressLine1);
            _driver.FindElement(AddressLine2).SendKeys(viewInfo.AddressLine2);
            _driver.FindElement(AddressLine3).SendKeys(viewInfo.AddressLine3);
            _driver.FindElement(Postcode).SendKeys(viewInfo.Postcode);
            _driver.FindElement(HomePhone).SendKeys(viewInfo.HomePhone);
            _driver.FindElement(Mobile).SendKeys(viewInfo.Mobile);

            _driver.FindElement(Save).Click();
        }

        public void UpdateMobile(string mobileNumber)
        {
            _driver.FindElement(Mobile).Clear();
            _driver.FindElement(Mobile).SendKeys(mobileNumber);
            _driver.FindElement(Save).Click();
        }

        public CustomerUiViewInfo GetViewInfo()
        {
            return new CustomerUiViewInfo(
                new SelectElement(_driver.WaitForElement(Title)).SelectedOption.Text,
                _driver.FindElement(Name).GetAttribute("value"),
                _driver.FindElement(AddressLine1).GetAttribute("value"),
                _driver.FindElement(AddressLine2).GetAttribute("value"),
                _driver.FindElement(AddressLine3).GetAttribute("value"),
                _driver.FindElement(Postcode).GetAttribute("value"),
                _driver.FindElement(HomePhone).GetAttribute("value"),
                _driver.FindElement(Mobile).GetAttribute("value"));
        }

        public bool HasNewCarLink()
        {
            var toolbar = new Toolbar(_driver);

            return toolbar.Links
                .Any(l => l.Url.Contains("carmaint.php") && l.AltText.Contains("Add a new car for"));
        }
    }
}
