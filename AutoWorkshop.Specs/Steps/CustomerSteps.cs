namespace AutoWorkshop.Specs.Steps
{
    using System.Linq;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Framework;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;

    [Binding]
    public class CustomerSteps
    {
        private readonly CustomerMaintenancePage _customerMaintenancePage;
        private readonly CustomerRepository _customerRepository;
        private CustomerUiViewInfo _uiViewInfo;
        private CustomerInfo _storedCustomer;

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

        [Given(@"this existing customer")]
        [Given(@"these customers")]
        public void GivenThisExistingCustomer(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _customerRepository.RemoveByName(values["Name"]);

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

                _customerRepository.Create(_storedCustomer);
            });
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

            _customerMaintenancePage.Open();
            _customerMaintenancePage.CreateCustomer(_uiViewInfo);
        }

        [When(@"I view the customer")]
        public void WhenIViewTheCustomer()
        {
            _storedCustomer.Should().NotBeNull();
            
            int customerId = _customerRepository.GetIdByName(_storedCustomer.Name);

            _customerMaintenancePage.Open(customerId);
            _uiViewInfo = _customerMaintenancePage.GetViewInfo();
        }

        [When(@"I update the customer with a new mobile number of '(.*)'")]
        public void WhenIUpdateTheCustomerWithANewMobileNumberOf(string newMobileNumber)
        {
            _storedCustomer.Should().NotBeNull();

            int customerId = _customerRepository.GetIdByName(_storedCustomer.Name);

            _customerMaintenancePage.Open(customerId);
            _customerMaintenancePage.UpdateMobile(newMobileNumber);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string searchText)
        {
            _customerMaintenancePage.Open();
            _customerMaintenancePage.EnterName(searchText);
        }

        [When(@"I select the option to create a new car for the customer")]
        public void WhenISelectTheOptionToCreateANewCarForTheCustomer()
        {
            _customerMaintenancePage.AddNewCar();
        }

        [Then(@"the customer is added to the system with the details provided")]
        public void ThenTheCustomerIsAddedToTheSystemWithTheDetailsProvided()
        {
            _uiViewInfo.Should().NotBeNull();

            _storedCustomer = _customerRepository.GetInfoByName(_uiViewInfo.Name);

            _storedCustomer.Should().NotBeNull();
            _storedCustomer.Title.Should().Be(_uiViewInfo.Title);
            _storedCustomer.Name.Should().Be(_uiViewInfo.Name);
            _storedCustomer.AddressLine1.Should().Be(_uiViewInfo.AddressLine1);
            _storedCustomer.AddressLine2.Should().Be(_uiViewInfo.AddressLine2);
            _storedCustomer.AddressLine3.Should().Be(_uiViewInfo.AddressLine3);
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

            var latestStoredCustomer = _customerRepository.GetInfoByName(_storedCustomer.Name);

            latestStoredCustomer.Mobile.Should().Be(expectedMobileNumber);
        }

        [Then(@"I should see the following toolbar options")]
        public void ThenIShouldSeeTheFollowingToolbarOptions(Table table)
        {
            table.Rows.ForEach(row =>
            {
                _customerMaintenancePage.Toolbar.FindButtonByAltText(row["Option"]).Should().NotBeNull();
            });
        }

        [Then(@"I should see the customer in the list of as-you-type results")]
        public void ThenIShouldSeeTheCustomerInTheListOfAsYouTypeResults()
        {
            _storedCustomer.Should().NotBeNull();

            bool foundExpectedCustomerInSearchResults = Poller.PollForResult(() =>
                _customerMaintenancePage.GetAsYouTypeSearchResults().Contains(_storedCustomer.Name));

            foundExpectedCustomerInSearchResults.Should().BeTrue();
        }
    }
}
