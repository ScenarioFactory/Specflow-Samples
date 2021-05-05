namespace AutoWorkshop.Specs.UI
{
    using System.Linq;
    using Dto;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class CustomerMaintenancePage : Page
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
        private static readonly By AsYouTypeSearchResultAnchors = By.XPath("//div[@id='matchingPanel']//a");

        public CustomerMaintenancePage(AutoWorkshopDriver driver) :base(driver)
        {
        }

        public void Open()
        {
            Driver.NavigateTo(PageUrl);
        }

        public void Open(int customerId)
        {
            Driver.NavigateTo($"{PageUrl}?custid={customerId}");
        }

        public void CreateCustomer(CustomerUiViewInfo viewInfo)
        {
            var titleSelectElement = new SelectElement(Driver.WaitForElement(Title));
            titleSelectElement.SelectByText(viewInfo.Title);

            Driver.FindElement(Name).SendKeys(viewInfo.Name);
            Driver.FindElement(AddressLine1).SendKeys(viewInfo.AddressLine1);
            Driver.FindElement(AddressLine2).SendKeys(viewInfo.AddressLine2);
            Driver.FindElement(AddressLine3).SendKeys(viewInfo.AddressLine3);
            Driver.FindElement(Postcode).SendKeys(viewInfo.Postcode);
            Driver.FindElement(HomePhone).SendKeys(viewInfo.HomePhone);
            Driver.FindElement(Mobile).SendKeys(viewInfo.Mobile);

            Driver.FindElement(Save).Click();
        }

        public void UpdateMobile(string mobileNumber)
        {
            Driver.FindElement(Mobile).Clear();
            Driver.FindElement(Mobile).SendKeys(mobileNumber);
            Driver.FindElement(Save).Click();
        }

        public CustomerUiViewInfo GetViewInfo()
        {
            return new CustomerUiViewInfo(
                new SelectElement(Driver.WaitForElement(Title)).SelectedOption.Text,
                Driver.FindElement(Name).GetAttribute("value"),
                Driver.FindElement(AddressLine1).GetAttribute("value"),
                Driver.FindElement(AddressLine2).GetAttribute("value"),
                Driver.FindElement(AddressLine3).GetAttribute("value"),
                Driver.FindElement(Postcode).GetAttribute("value"),
                Driver.FindElement(HomePhone).GetAttribute("value"),
                Driver.FindElement(Mobile).GetAttribute("value"));
        }

        public void EnterName(string searchText)
        {
            Driver.FindElement(Name).SendKeys(searchText);
        }

        public string[] GetAsYouTypeSearchResults()
        {
            var anchors = Driver.FindElements(AsYouTypeSearchResultAnchors);

            return anchors
                .Where((a, i) => i % 2 != 0)
                .Select(a => a.Text)
                .ToArray();
        }

        public void AddNewCar()
        {
            Toolbar.FindButtonByAltText("Add a new car").Click();
        }
    }
}
