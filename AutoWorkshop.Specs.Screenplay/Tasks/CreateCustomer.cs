namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Drivers;
    using OpenQA.Selenium.Support.UI;
    using Pages;
    using Screenplay;

    public class CreateCustomer : WebTask
    {
        private readonly string _name;
        private string _title;
        private string _addressLine1;
        private string _addressLine2;
        private string _addressLine3;
        private string _postcode;
        private string _homePhone;
        private string _mobile;

        private CreateCustomer(string name)
        {
            _name = name;
        }

        public static CreateCustomer Named(string name)
        {
            return new CreateCustomer(name);
        }

        public CreateCustomer WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public CreateCustomer WithAddress(string addressLine1, string addressLine2, string addressLine3, string postcode)
        {
            _addressLine1 = addressLine1;
            _addressLine2 = addressLine2;
            _addressLine3 = addressLine3;
            _postcode = postcode;
            return this;
        }

        public CreateCustomer WithHomePhone(string homePhone)
        {
            _homePhone = homePhone;
            return this;
        }

        public CreateCustomer WithMobile(string mobile)
        {
            _mobile = mobile;
            return this;
        }

        public override void PerformAs(Actor actor, AutoWorkshopDriver driver)
        {
            var titleSelectElement = new SelectElement(driver.WaitForElement(CustomerMaintenancePage.Title));
            titleSelectElement.SelectByText(_title);

            driver.FindElement(CustomerMaintenancePage.Name).SendKeys(_name);
            driver.FindElement(CustomerMaintenancePage.AddressLine1).SendKeys(_addressLine1);
            driver.FindElement(CustomerMaintenancePage.AddressLine2).SendKeys(_addressLine2);
            driver.FindElement(CustomerMaintenancePage.AddressLine3).SendKeys(_addressLine3);
            driver.FindElement(CustomerMaintenancePage.Postcode).SendKeys(_postcode);
            driver.FindElement(CustomerMaintenancePage.HomePhone).SendKeys(_homePhone);
            driver.FindElement(CustomerMaintenancePage.Mobile).SendKeys(_mobile);

            driver.FindElement(CustomerMaintenancePage.Save).Click();
        }
    }
}
