namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Database;
    using Database.Tasks;
    using Framework;
    using Pattern;
    using TechTalk.SpecFlow;

    [Binding]
    public class SystemSteps
    {
        private readonly IActor _actor;

        public SystemSteps(AppSettings appSettings)
        {
            _actor = new Actor().WhoCan(
                UseMySqlDatabase.With(appSettings.MySqlConnectionString));
        }

        [Given(@"the date is '(.*)'")]
        public void GivenTheDateIs(ScenarioDate currentDate)
        {
            _actor.AttemptsTo(SetCurrentDate.To(currentDate));
        }
    }
}
