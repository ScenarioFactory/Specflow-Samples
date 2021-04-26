namespace AutoWorkshop.Specs.Steps
{
    using System.Linq;
    using Dto;
    using FluentAssertions;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;

    [Binding]
    public class CustomerSteps
    {
        private CustomerUiInput _customerUiInput;
        private CustomerInfo _retrievedCustomerInfo;

        [Given(@"there are no customers named '(.*)'")]
        public void GivenThereAreNoCustomersNamed(string name)
        {
            CustomerRepository.RemoveByName(name);
        }

        [When(@"the user creates a new customer with the following details")]
        public void WhenTheUserCreatesANewCustomerWithTheFollowingDetails(Table table)
        {
            var values = table.Rows.Single();

            _customerUiInput = new CustomerUiInput(
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

            page.CreateCustomer(_customerUiInput);
        }

        [Then(@"the customer is added to the system")]
        public void ThenTheCustomerIsAddedToTheSystem()
        {
            _customerUiInput.Should().NotBeNull();

            _retrievedCustomerInfo = CustomerRepository.FindByName(_customerUiInput.Name);

            _retrievedCustomerInfo.Should().NotBeNull();
            _retrievedCustomerInfo.Title.Should().Be(_customerUiInput.Title);
            _retrievedCustomerInfo.Name.Should().Be(_customerUiInput.Name);
            _retrievedCustomerInfo.AddressLine1.Should().Be(_customerUiInput.AddressLine1);
            _retrievedCustomerInfo.AddressLine2.Should().Be(_customerUiInput.AddressLine2);
            _retrievedCustomerInfo.AddressLine2.Should().Be(_customerUiInput.AddressLine2);
            _retrievedCustomerInfo.Postcode.Should().Be(_customerUiInput.Postcode);
            _retrievedCustomerInfo.HomePhone.Should().Be(_customerUiInput.HomePhone);
            _retrievedCustomerInfo.Mobile.Should().Be(_customerUiInput.Mobile);
        }

        [Then(@"the customer is marked as manually invoiced")]
        public void ThenTheCustomerIsMarkedAsManuallyInvoiced()
        {
            _retrievedCustomerInfo.Should().NotBeNull();

            _retrievedCustomerInfo.IsAccountInvoicing.Should().BeFalse();
        }
    }
}
