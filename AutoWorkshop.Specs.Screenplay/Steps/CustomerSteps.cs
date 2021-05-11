namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Abilities;
    using Actors;
    using Database.Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public class CustomerSteps
    {
        private readonly Actor _actor;

        public CustomerSteps(AppSettings appSettings)
        {
            _actor = new Actor();
            _actor.Can(UseMySqlDatabase.With(appSettings.MySqlConnectionString));
        }

        [Given(@"there are no customers named '(.*)'")]
        public void GivenThereAreNoCustomersNamed(string customerName)
        {
            _actor.AttemptsTo(DeleteCustomers.ByName(customerName));
        }
    }
}