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
    public class CarSteps
    {
        private readonly CarMaintenancePage _carMaintenancePage;
        private readonly ChangeCarRegistrationPage _changeCarRegistrationPage;
        private readonly CarRepository _carRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly JobRepository _jobRepository;
        private CarUiViewInfo _uiViewInfo;

        public CarSteps(
            CarMaintenancePage carMaintenancePage,
            ChangeCarRegistrationPage changeCarRegistrationPage,
            CarRepository carRepository,
            CustomerRepository customerRepository,
            JobRepository jobRepository)
        {
            _carMaintenancePage = carMaintenancePage;
            _changeCarRegistrationPage = changeCarRegistrationPage;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _jobRepository = jobRepository;
        }

        [Given(@"this existing car")]
        [Given(@"these existing cars")]
        [Given(@"these cars")]
        public void GivenTheseExistingCars(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _carRepository.RemoveByRegistration(values["Registration"]);

                int customerId = values.ContainsKey("Customer") ?
                    _customerRepository.GetIdByName(values["Customer"]) : _customerRepository.GetFirstCustomerId();

                var car = new CarInfo(
                    values["Registration"],
                    customerId,
                    values["Make"],
                    values["Model"]);

                _carRepository.Create(car);
            });
        }

        [Given(@"there is no existing car with registration '(.*)'")]
        public void GivenThereIsNoExistingCarWithRegistration(string registration)
        {
            _carRepository.RemoveByRegistration(registration);
            _jobRepository.RemoveByRegistration(registration);
        }

        [When(@"I change the registration of '(.*)' to '(.*)'")]
        public void WhenIChangeTheRegistrationOfTo(string currentRegistration, string newRegistration)
        {
            _changeCarRegistrationPage.Open();
            _changeCarRegistrationPage.ChangeRegistration(currentRegistration, newRegistration);
        }

        [When(@"I create a new car for customer '(.*)' with the following details")]
        public void WhenICreateANewCarForCustomerWithTheFollowingDetails(string customerName, Table table)
        {
            var values = table.Rows.Single();

            _uiViewInfo = new CarUiViewInfo(
                values["Registration"],
                values["Make"],
                values["Model"],
                values["Year"]);

            _carMaintenancePage.CreateCar(_uiViewInfo);
        }

        [When(@"I select the option to create a new job for the car")]
        public void WhenISelectTheOptionToCreateANewJobForTheCar()
        {
            _carMaintenancePage.AddNewJob();
        }

        [Then(@"the car is added to the system with the details provided")]
        public void ThenTheCarIsAddedToTheSystemWithTheDetailsProvided()
        {
            _uiViewInfo.Should().NotBeNull();

            CarInfo storedCar = _carRepository.GetInfoByRegistration(_uiViewInfo.Registration);

            storedCar.Registration.Should().Be(_uiViewInfo.Registration);
            storedCar.Make.Should().Be(_uiViewInfo.Make);
            storedCar.Model.Should().Be(_uiViewInfo.Model);
        }

        [Then(@"I should see the success message '(.*)'")]
        public void ThenIShouldSeeTheSuccessMessage(string expectedMessage)
        {
            string successMessage = _changeCarRegistrationPage.GetSuccessMessage();

            successMessage.Should().Be(expectedMessage);
        }

        [Then(@"I should see the error message '(.*)'")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            string errorMessage = _changeCarRegistrationPage.GetErrorMessage();

            errorMessage.Should().Be(expectedMessage);
        }

        [Then(@"the following car should be present in the system")]
        [Then(@"the following cars should be present in the system")]
        public void ThenTheFollowingCarsShouldBePresentInTheSystem(Table table)
        {
            table.Rows.ForEach(expectedValues =>
            {
                CarInfo storedCar = _carRepository.GetInfoByRegistration(expectedValues["Registration"]);

                storedCar.Should().NotBeNull();
                storedCar.Registration.Should().Be(expectedValues["Registration"]);
                storedCar.Make.Should().Be(expectedValues["Make"]);
                storedCar.Model.Should().Be(expectedValues["Model"]);
            });
        }

        [Then(@"there should be no car with registration '(.*)'")]
        public void ThenThereShouldBeNoCarWithRegistration(string registration)
        {
            CarInfo storedCar = _carRepository.GetInfoByRegistration(registration);

            storedCar.Should().BeNull();
        }
    }
}
