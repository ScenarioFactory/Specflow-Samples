namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using System.Linq;
    using Abilities;
    using Actors;
    using Drivers;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Repositories;
    using Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public class JobUiSteps
    {
        private readonly JobRepository _jobRepository;
        private JobUiViewInfo _uiViewInfo;
        private readonly Actor _actor;

        public JobUiSteps(AutoWorkshopDriver driver, JobRepository jobRepository)
        {
            _jobRepository = jobRepository;

            _actor = new Actor();
            _actor.Can(UseAutoWorkshop.With(driver));
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

        [Then(@"the job is added to the system with the details provided")]
        public void ThenTheJobIsAddedToTheSystemWithTheDetailsProvided()
        {
            _uiViewInfo.Should().NotBeNull();

            JobInfo[] storedJobs = _jobRepository.GetByRegistration(_uiViewInfo.Registration);

            JobInfo matchingJob = storedJobs.SingleOrDefault(j =>
                j.Registration == _uiViewInfo.Registration &&
                j.Description == _uiViewInfo.Description &&
                j.Date == _uiViewInfo.Date &&
                j.Hours == _uiViewInfo.Hours &&
                j.Mileage == _uiViewInfo.Mileage);

            matchingJob.Should().NotBeNull();
        }
    }
}
