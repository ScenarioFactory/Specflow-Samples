namespace AutoWorkshop.Specs.Steps
{
    using System.Linq;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;

    [Binding]
    public class JobSteps
    {
        private readonly JobMaintenancePage _jobMaintenancePage;
        private readonly JobRepository _jobRepository;
        private JobUiViewInfo _uiViewInfo;

        public JobSteps(JobMaintenancePage jobMaintenancePage, JobRepository jobRepository)
        {
            _jobMaintenancePage = jobMaintenancePage;
            _jobRepository = jobRepository;
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

            _jobMaintenancePage.CreateJob(_uiViewInfo);
        }

        [Then(@"the job should be added to the system with the details provided")]
        public void ThenTheJobShouldBeAddedToTheSystemWithTheDetailsProvided()
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
