namespace AutoWorkshop.Specs.Stateless.Steps
{
    using System.Linq;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Framework;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;

    [Binding]
    public class CarSteps
    {
        private readonly CarRepository _carRepository;
        private readonly JobRepository _jobRepository;
        private readonly CarMaintenancePage _carMaintenancePage;

        public CarSteps(CarRepository carRepository, JobRepository jobRepository, CarMaintenancePage carMaintenancePage)
        {
            _carRepository = carRepository;
            _jobRepository = jobRepository;
            _carMaintenancePage = carMaintenancePage;
        }

        [Given(@"there is no existing car or jobs for registration '(.*)'")]
        public void GivenThereIsNoExistingCarOrJobsForRegistration(string registration)
        {
            _carRepository.RemoveByRegistration(registration);
            _jobRepository.RemoveByRegistration(registration);
        }

        [When(@"I create a new car for customer '(.*) with the following details")]
        public void WhenICreateANewCarForCustomerWithTheFollowingDetails(TransformedInt customerId, Table table)
        {
            var values = table.Rows.Single();

            var uiViewInfo = new CarUiViewInfo(
                values["Registration"],
                customerId, 
                values["Make"],
                values["Model"],
                values["Year"]);

            _carMaintenancePage.Open();
            _carMaintenancePage.CreateCar(uiViewInfo);
        }

        [Then(@"a car is present in the system with the following details")]
        public void ThenACarIsPresentInTheSystemWithTheFollowingDetails(TransformedTable table)
        {
            var expectedValues = table.Rows.Single();

            CarInfo storedCar = _carRepository.GetInfoByRegistration(expectedValues["Registration"]);

            storedCar.Registration.Should().Be(expectedValues["Registration"]);
            storedCar.CustomerId.Should().Be(expectedValues.GetInt("Customer"));
            storedCar.Make.Should().Be(expectedValues["Make"]);
            storedCar.Model.Should().Be(expectedValues["Model"]);
        }
    }
}
