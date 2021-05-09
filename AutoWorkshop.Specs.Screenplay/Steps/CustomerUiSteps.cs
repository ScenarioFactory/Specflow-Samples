namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using System.Linq;
    using Abilities;
    using Drivers;
    using Dto;
    using FluentAssertions;
    using Repositories;
    using Screenplay;
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

        [When(@"I create a new customer with the following details")]
        public void WhenActorCreatesANewCustomerWithTheFollowingDetails(Table table)
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
    }
}
