namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using System.Linq;
    using Database;
    using Database.Questions;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Framework;
    using Pattern;
    using TechTalk.SpecFlow;
    using WebDriver;
    using WebDriver.Tasks;

    [Binding]
    public class JobUiSteps
    {
        private readonly IActor _actor;
        private JobUiViewInfo _uiViewInfo;

        public JobUiSteps(AppSettings appSettings, AutoWorkshopDriver driver)
        {
            _actor = new Actor().WhoCan(
                UseAutoWorkshop.With(driver),
                UseMySqlDatabase.With(appSettings.MySqlConnectionString));
        }

        [When(@"I create the following job for car '(.*)'")]
        public void WhenICreateTheFollowingJobForCar(string registration, Table table)
        {
            var values = table.Rows.Single();

            _uiViewInfo = new JobUiViewInfo(
                registration,
                values["Description"],
                values.GetDate("Date"),
                values.GetDecimal("Hours"),
                values.GetInt("Mileage"));

            _actor.AttemptsTo(
                CreateJob.WithDescription(_uiViewInfo.Description)
                    .OnDate(_uiViewInfo.Date)
                    .TakingHours(_uiViewInfo.Hours)
                    .AtMileage(_uiViewInfo.Mileage));
        }

        [Then(@"the job should be added to the system with the details provided")]
        public void ThenTheJobShouldBeAddedToTheSystemWithTheDetailsProvided()
        {
            _uiViewInfo.Should().NotBeNull();

            JobInfo matchingJob = null;

            Poller.PollForSuccess(() =>
            {
                JobInfo[] storedJobs = _actor.AsksFor(StoredJobs.ForRegistration(_uiViewInfo.Registration));

                matchingJob = storedJobs.SingleOrDefault(j =>
                    j.Registration == _uiViewInfo.Registration &&
                    j.Description == _uiViewInfo.Description &&
                    j.Date == _uiViewInfo.Date &&
                    j.Hours == _uiViewInfo.Hours &&
                    j.Mileage == _uiViewInfo.Mileage);

                return matchingJob != null;
            });

            matchingJob.Should().NotBeNull();
        }
    }
}
