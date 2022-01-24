namespace AutoWorkshop.Specs.UI
{
    using System.Linq;
    using Dto;
    using Framework;
    using OpenQA.Selenium;

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
        private static readonly By AsYouTypeSearchResultAnchors = By.XPath("//div[@id='matchingPanel']//a");

        private readonly AutoWorkshopDriver _driver;

        public CustomerMaintenancePage(AutoWorkshopDriver driver, Toolbar toolbar)
        {
            _driver = driver;
            Toolbar = toolbar;
        }

        public Toolbar Toolbar { get; }

        public void Open()
        {
            _driver.NavigateTo(PageUrl);
        }

        public void Open(int customerId)
        {
            _driver.NavigateTo($"{PageUrl}?custid={customerId}");
        }

        public void CreateCustomer(CustomerUiViewInfo viewInfo)
        {
            _driver.SelectOptionByTextWhenVisible(Title, viewInfo.Title);

            _driver.SendKeysWhenVisible(Name, viewInfo.Name);
            _driver.SendKeysWhenVisible(AddressLine1, viewInfo.AddressLine1);
            _driver.SendKeysWhenVisible(AddressLine2, viewInfo.AddressLine2);
            _driver.SendKeysWhenVisible(AddressLine3, viewInfo.AddressLine3);
            _driver.SendKeysWhenVisible(Postcode, viewInfo.Postcode);
            _driver.SendKeysWhenVisible(HomePhone, viewInfo.HomePhone);
            _driver.SendKeysWhenVisible(Mobile, viewInfo.Mobile);

            _driver.ClickElementWhenClickable(Save);
        }

        public void UpdateMobile(string mobileNumber)
        {
            _driver.SendKeysWhenVisible(Mobile, mobileNumber);

            _driver.ClickElementWhenClickable(Save);
        }

        public CustomerUiViewInfo GetViewInfo()
        {
            return new CustomerUiViewInfo(
                _driver.GetSelectedOptionTextWhenVisible(Title),
                _driver.GetElementAttributeValueWhenVisible(Name, "value"),
                _driver.GetElementAttributeValueWhenVisible(AddressLine1, "value"),
                _driver.GetElementAttributeValueWhenVisible(AddressLine2, "value"),
                _driver.GetElementAttributeValueWhenVisible(AddressLine3, "value"),
                _driver.GetElementAttributeValueWhenVisible(Postcode, "value"),
                _driver.GetElementAttributeValueWhenVisible(HomePhone, "value"),
                _driver.GetElementAttributeValueWhenVisible(Mobile, "value"));
        }

        public void EnterName(string searchText)
        {
            _driver.SendKeysWhenVisible(Name, searchText);
        }

        public string[] GetAsYouTypeSearchResults()
        {
            string[] textValues = _driver.GetMultipleElementTextValuesWhenVisible(AsYouTypeSearchResultAnchors);

            return textValues
                .Where((a, i) => i % 2 != 0) // get every other value
                .ToArray();
        }

        public void AddNewCar()
        {
            Toolbar.ClickButtonByAltText("Add a new car");
        }
    }
}
