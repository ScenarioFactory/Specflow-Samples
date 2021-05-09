namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using System.Linq;
    using Abilities;
    using Drivers;
    using Screenplay;
    using Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public class CustomerUiSteps
    {
        private readonly Actor _actor;

        public CustomerUiSteps(AutoWorkshopDriver driver)
        {
            _actor = new Actor();
            _actor.Can(UseAutoWorkshop.With(driver));
        }

        [When(@"I create a new customer with the following details")]
        public void WhenActorCreatesANewCustomerWithTheFollowingDetails(Table table)
        {
            var values = table.Rows.Single();

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
    }
}
