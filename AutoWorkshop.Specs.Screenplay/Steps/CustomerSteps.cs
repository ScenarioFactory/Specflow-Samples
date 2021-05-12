namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Database;
    using Database.Tasks;
    using Extensions;
    using Pattern;
    using TechTalk.SpecFlow;

    [Binding]
    public class CustomerSteps
    {
        private readonly IActor _actor;

        public CustomerSteps(AppSettings appSettings)
        {
            _actor = new Actor().WhoCan(
                UseMySqlDatabase.With(appSettings.MySqlConnectionString));
        }

        [Given(@"there are no customers named '(.*)'")]
        public void GivenThereAreNoCustomersNamed(string customerName)
        {
            _actor.AttemptsTo(DeleteCustomers.WithName(customerName));
        }

        [Given(@"the following customers")]
        public void GivenTheFollowingCustomers(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _actor.AttemptsTo(
                    DeleteCustomers.WithName(values["Name"]),
                    InsertCustomer.Named(values["Name"])
                        .WithTitle(values["Title"])
                        .WithAddress(
                            values["Address Line 1"],
                            values["Address Line 2"],
                            values["Address Line 3"],
                            values["Postcode"])
                        .WithHomePhone(values["Home Phone"])
                        .WithMobile(values["Mobile"]));
            });
        }
    }
}