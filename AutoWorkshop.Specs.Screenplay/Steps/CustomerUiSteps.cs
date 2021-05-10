namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using System.Linq;
    using Abilities;
    using Drivers;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Pages;
    using Questions;
    using Repositories;
    using Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public class CustomerUiSteps
    {
        private readonly Actor _actor;
        private readonly  CustomerRepository _customerRepository;
        private CustomerUiViewInfo _uiViewInfo;
        private CustomerInfo _storedCustomer;

        public CustomerUiSteps(AutoWorkshopDriver driver, CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            _actor = new Actor();
            _actor.Can(UseAutoWorkshop.With(driver));
        }

        [Given(@"this existing customer")]
        public void GivenThisExistingCustomer(Table table)
        {
            var values = table.Rows.Single();

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

            _actor.AttemptsTo(
                Navigate.ToCustomerMaintenance(),
                CreateCustomer
                    .Named(values["Name"])
                    .WithTitle(values["Title"])
                    .WithAddress(
                        values["Address Line 1"],
                        values["Address Line 2"],
                        values["Address Line 3"],
                        values["Postcode"])
                    .WithHomePhone(values["Home Phone"])
                    .WithMobile(values["Mobile"]));
        }

        [When(@"I view the customer")]
        public void WhenIViewTheCustomer()
        {
            _storedCustomer.Should().NotBeNull();

            int customerId = _customerRepository.GetIdByName(_storedCustomer.Name);

            _actor.AttemptsTo(ViewCustomer.WithId(customerId));
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
            _storedCustomer.Should().NotBeNull();

            _actor.AsksFor(SelectedOptionText.Of(CustomerMaintenancePage.Title)).Should().Be(_storedCustomer.Title);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.Name)).Should().Be(_storedCustomer.Name);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.AddressLine1)).Should().Be(_storedCustomer.AddressLine1);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.AddressLine2)).Should().Be(_storedCustomer.AddressLine2);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.AddressLine3)).Should().Be(_storedCustomer.AddressLine3);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.Postcode)).Should().Be(_storedCustomer.Postcode);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.HomePhone)).Should().Be(_storedCustomer.HomePhone);
            _actor.AsksFor(Text.Of(CustomerMaintenancePage.Mobile)).Should().Be(_storedCustomer.Mobile);
        }

        [Then(@"I should see the following toolbar options")]
        public void ThenIShouldSeeTheFollowingToolbarOptions(Table table)
        {
            ToolbarButtonInfo[] toolbarButtons = _actor.AsksFor(ToolbarButtons.CurrentlyVisible());

            table.Rows.ForEach(row =>
            {
                bool buttonIsVisible = toolbarButtons.Any(b => b.AltText == row["Option"]);
                buttonIsVisible.Should().BeTrue($"button for '{row["Option"]}' should be visible");
            });
        }
    }
}
