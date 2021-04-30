namespace AutoWorkshop.Specs.Steps
{
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Repositories;
    using TechTalk.SpecFlow;
    using UI;

    [Binding]
    public class CarSteps
    {
        private readonly ChangeCarRegistrationPage _changeCarRegistrationPage;
        private readonly CarRepository _carRepository;
        private readonly CustomerRepository _customerRepository;

        public CarSteps(ChangeCarRegistrationPage changeCarRegistrationPage, CarRepository carRepository, CustomerRepository customerRepository)
        {
            _changeCarRegistrationPage = changeCarRegistrationPage;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }

        [Given(@"this existing car")]
        [Given(@"these existing cars")]
        public void GivenTheseExistingCars(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _carRepository.RemoveByRegistration(values["Registration"]);

                uint customerId = _customerRepository.GetFirstCustomerId();

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
        }

        [When(@"I change the registration of '(.*)' to '(.*)'")]
        public void WhenIChangeTheRegistrationOfTo(string currentRegistration, string newRegistration)
        {
            _changeCarRegistrationPage.ChangeRegistration(currentRegistration, newRegistration);
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
