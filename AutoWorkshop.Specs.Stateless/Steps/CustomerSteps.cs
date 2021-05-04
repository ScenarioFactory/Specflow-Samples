namespace AutoWorkshop.Specs.Stateless.Steps
{
    using System.Linq;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;
    using Utilities;

    [Binding]
    public class CustomerSteps
    {
        private readonly CustomerMaintenancePage _customerMaintenancePage;
        private readonly CustomerRepository _customerRepository;

        public CustomerSteps(CustomerMaintenancePage customerMaintenancePage, CustomerRepository customerRepository)
        {
            _customerMaintenancePage = customerMaintenancePage;
            _customerRepository = customerRepository;
        }

        [Given(@"there are no customers named '(.*)'")]
        public void GivenThereAreNoCustomersNamed(string customerName)
        {
            _customerRepository.RemoveByName(customerName);
        }

        [Given(@"the following customer is present in the system")]
        public void GivenTheFollowingCustomerIsPresentInTheSystem(Table table)
        {
            var values = table.Rows.Single();

            var customer = new CustomerInfo(
                values["Title"],
                values["Name"],
                values["Address Line 1"],
                values["Address Line 2"],
                values["Address Line 3"],
                values["Postcode"],
                values["Home Phone"],
                values["Mobile"],
                1);

            _customerRepository.Create(customer);
        }

        [When(@"I create a new customer with the following details")]
        public void WhenICreateANewCustomerWithTheFollowingDetails(Table table)
        {
            var values = table.Rows.Single();

            var uiViewInfo = new CustomerUiViewInfo(
                values["Title"],
                values["Name"],
                values["Address Line 1"],
                values["Address Line 2"],
                values["Address Line 3"],
                values["Postcode"],
                values["Home Phone"],
                values["Mobile"]);

            _customerMaintenancePage.Open();
            _customerMaintenancePage.CreateCustomer(uiViewInfo);
        }

        [When(@"I view customer '(.*)'")]
        public void WhenIViewCustomer(string customerName)
        {
            int customerId = _customerRepository.GetIdByName(customerName);

            _customerMaintenancePage.Open(customerId);
        }

        [When(@"I update customer '(.*)' with a new mobile number of '(.*)'")]
        public void WhenIUpdateCustomerWithANewMobileNumberOf(string customerName, string newMobileNumber)
        {
            int customerId = _customerRepository.GetIdByName(customerName);

            _customerMaintenancePage.Open(customerId);
            _customerMaintenancePage.UpdateMobile(newMobileNumber);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string searchText)
        {
            _customerMaintenancePage.Open();
            _customerMaintenancePage.TypeName(searchText);
        }

        [Then(@"a customer is present in the system with the following details")]
        public void ThenACustomerIsPresentInTheSystemWithTheFollowingDetails(Table table)
        {
            var values = table.Rows.Single();
            var storedCustomer = _customerRepository.GetInfoByName(values["Name"]);

            storedCustomer.Should().NotBeNull();
            storedCustomer.Title.Should().Be(values["Title"]);
            storedCustomer.Name.Should().Be(values["Name"]);
            storedCustomer.AddressLine1.Should().Be(values["Address Line 1"]);
            storedCustomer.AddressLine2.Should().Be(values["Address Line 2"]);
            storedCustomer.AddressLine3.Should().Be(values["Address Line 3"]);
            storedCustomer.Postcode.Should().Be(values["Postcode"]);
            storedCustomer.HomePhone.Should().Be(values["Home Phone"]);
            storedCustomer.Mobile.Should().Be(values["Mobile"]);
        }

        [Then(@"customer '(.*)' is marked as manually invoiced")]
        public void ThenCustomerIsMarkedAsManuallyInvoiced(string customerName)
        {
            var storedCustomer = _customerRepository.GetInfoByName(customerName);

            storedCustomer.HasAccountInvoicing.Should().BeFalse();
        }

        [Then(@"I should see the following customer details")]
        public void ThenIShouldSeeTheFollowingCustomerDetails(Table table)
        {
            var expectedValues = table.Rows.Single();
            CustomerUiViewInfo uiViewInfo = _customerMaintenancePage.GetViewInfo();

            uiViewInfo.Title.Should().Be(expectedValues["Title"]);
            uiViewInfo.Name.Should().Be(expectedValues["Name"]);
            uiViewInfo.AddressLine1.Should().Be(expectedValues["Address Line 1"]);
            uiViewInfo.AddressLine2.Should().Be(expectedValues["Address Line 2"]);
            uiViewInfo.AddressLine3.Should().Be(expectedValues["Address Line 3"]);
            uiViewInfo.Postcode.Should().Be(expectedValues["Postcode"]);
            uiViewInfo.HomePhone.Should().Be(expectedValues["Home Phone"]);
            uiViewInfo.Mobile.Should().Be(expectedValues["Mobile"]);
        }

        [Then(@"I should see the following toolbar options")]
        public void ThenIShouldSeeTheFollowingToolbarOptions(Table table)
        {
            table.Rows.ForEach(row =>
            {
                _customerMaintenancePage.Toolbar.ContainsLink(row["Option"]).Should().BeTrue();
            });
        }

        [Then(@"customer '(.*)' should be have a mobile number of '(.*)'")]
        public void ThenCustomerShouldBeHaveAMobileNumberOf(string customerName, string expectedMobileNumber)
        {
            var storedCustomer = _customerRepository.GetInfoByName(customerName);

            storedCustomer.Mobile.Should().Be(expectedMobileNumber);
        }

        [Then(@"I should see '(.*)' in the list of as-you-type results")]
        public void ThenIShouldSeeInTheListOfAs_You_TypeResults(string customerName)
        {
            bool foundExpectedCustomerInSearchResults = Poller.PollForResult(() =>
                _customerMaintenancePage.GetAsYouTypeSearchResults().Contains(customerName));

            foundExpectedCustomerInSearchResults.Should().BeTrue();
        }
    }
}
