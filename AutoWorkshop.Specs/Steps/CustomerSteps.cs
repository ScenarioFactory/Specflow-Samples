namespace AutoWorkshop.Specs.Steps
{
    using System.Linq;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Infrastructure;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;

    [Binding]
    public class CustomerSteps
    {
        private CustomerUiViewInfo _uiViewInfo;
        private CustomerInfo _storedCustomer;
        private AutoWorkshopDriver _driver;
        private CustomerMaintenancePage _customerMaintenancePage;

        [AfterScenario("MaintainCustomers")]
        public void DisposeWebDriver() => _driver?.Quit();

        [Given(@"there are no customers named '(.*)'")]
        public void GivenThereAreNoCustomersNamed(string customerName)
        {
            CustomerRepository.RemoveByName(customerName);
        }

        [When(@"I create a new customer with the following details")]
        public void WhenICreateANewCustomerWithTheFollowingDetails(Table table)
        {
            var values = table.Rows.Single();

            _uiViewInfo = new CustomerUiViewInfo(
                values["Title"],
                values["Name"],
                values["Address Line 1"],
                values["Address Line 2"],
                values["Address Line 3"],
                values["Postcode"],
                values["Home Phone"],
                values["Mobile"]);

            using var driver = AutoWorkshopDriver.CreateAuthenticatedInstance();
            var page = new CustomerMaintenancePage(driver);

            page.CreateCustomer(_uiViewInfo);
        }

        [Given(@"the following existing customer")]
        public void GivenTheFollowingExistingCustomer(Table table)
        {
            var values = table.Rows.Single();

            CustomerRepository.RemoveByName(values["Name"]);

            _storedCustomer = new CustomerInfo(
                values["Title"],
                values["Name"],
                values["Address Line 1"],
                values["Address Line 2"],
                values["Address Line 3"],
                values["Postcode"],
                values["Home Phone"],
                values["Mobile"],
                1);

            CustomerRepository.Create(_storedCustomer);
        }

        [When(@"I view the customer")]
        public void WhenIViewTheCustomer()
        {
            _storedCustomer.Should().NotBeNull();
            
            int customerId = CustomerRepository.GetIdByName(_storedCustomer.Name);

            _driver = AutoWorkshopDriver.CreateAuthenticatedInstance();
            _customerMaintenancePage = new CustomerMaintenancePage(_driver, customerId);

            _uiViewInfo = _customerMaintenancePage.GetViewInfo();
        }

        [When(@"I update the customer with a new mobile number of '(.*)'")]
        public void WhenIUpdateTheCustomerWithANewMobileNumberOf(string newMobileNumber)
        {
            _storedCustomer.Should().NotBeNull();

            int customerId = CustomerRepository.GetIdByName(_storedCustomer.Name);

            using var driver = AutoWorkshopDriver.CreateAuthenticatedInstance();
            var page = new CustomerMaintenancePage(driver, customerId);

            page.UpdateMobile(newMobileNumber);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string searchText)
        {
            _driver = AutoWorkshopDriver.CreateAuthenticatedInstance();
            _customerMaintenancePage = new CustomerMaintenancePage(_driver);

            _customerMaintenancePage.TypeName(searchText);
        }

        [Then(@"the customer is added to the system with the details provided")]
        public void ThenTheCustomerIsAddedToTheSystemWithTheDetailsProvided()
        {
            _uiViewInfo.Should().NotBeNull();

            _storedCustomer = CustomerRepository.GetInfoByName(_uiViewInfo.Name);

            _storedCustomer.Should().NotBeNull();
            _storedCustomer.Title.Should().Be(_uiViewInfo.Title);
            _storedCustomer.Name.Should().Be(_uiViewInfo.Name);
            _storedCustomer.AddressLine1.Should().Be(_uiViewInfo.AddressLine1);
            _storedCustomer.AddressLine2.Should().Be(_uiViewInfo.AddressLine2);
            _storedCustomer.AddressLine2.Should().Be(_uiViewInfo.AddressLine2);
            _storedCustomer.Postcode.Should().Be(_uiViewInfo.Postcode);
            _storedCustomer.HomePhone.Should().Be(_uiViewInfo.HomePhone);
            _storedCustomer.Mobile.Should().Be(_uiViewInfo.Mobile);
        }

        [Then(@"the customer is marked as manually invoiced")]
        public void ThenTheCustomerIsMarkedAsManuallyInvoiced()
        {
            _storedCustomer.Should().NotBeNull();

            _storedCustomer.HasAccountInvoicing.Should().BeFalse();
        }

        [Then(@"I should see the stored customer details")]
        public void ThenIShouldSeeTheStoredCustomerDetails()
        {
            _uiViewInfo.Should().NotBeNull();
            _storedCustomer.Should().NotBeNull();

            _uiViewInfo.Title.Should().Be(_storedCustomer.Title);
            _uiViewInfo.Name.Should().Be(_storedCustomer.Name);
            _uiViewInfo.AddressLine1.Should().Be(_storedCustomer.AddressLine1);
            _uiViewInfo.AddressLine2.Should().Be(_storedCustomer.AddressLine2);
            _uiViewInfo.Postcode.Should().Be(_storedCustomer.Postcode);
            _uiViewInfo.HomePhone.Should().Be(_storedCustomer.HomePhone);
            _uiViewInfo.Mobile.Should().Be(_storedCustomer.Mobile);
        }

        [Then(@"the stored customer should be updated with mobile '(.*)'")]
        public void ThenTheStoredCustomerShouldBeUpdatedWithMobile(string expectedMobileNumber)
        {
            _storedCustomer.Should().NotBeNull();

            var latestStoredCustomer = CustomerRepository.GetInfoByName(_storedCustomer.Name);

            latestStoredCustomer.Mobile.Should().Be(expectedMobileNumber);
        }

        [Then(@"I should see the following toolbar options")]
        public void ThenIShouldSeeTheFollowingToolbarOptions(Table table)
        {
            _customerMaintenancePage.Should().NotBeNull();

            table.Rows.ForEach(row =>
            {
                _customerMaintenancePage.Toolbar.ContainsLink(row["Option"]).Should().BeTrue();
            });
        }

        [Then(@"I should see the customer in the list of as-you-type results")]
        public void ThenIShouldSeeTheCustomerInTheListOfAsYouTypeResults()
        {
            _storedCustomer.Should().NotBeNull();
            _customerMaintenancePage.Should().NotBeNull();

            bool foundExpectedCustomerInSearchResults = Poller.PollForResult(() =>
                _customerMaintenancePage.GetAsYouTypeSearchResults().Contains(_storedCustomer.Name));

            foundExpectedCustomerInSearchResults.Should().BeTrue();
        }
    }
}
