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
        private AutoWorkshopDriver _driver;
        private ChangeCarRegistrationPage _changeCarRegistrationPage;

        [Given(@"this existing car")]
        [Given(@"these existing cars")]
        public void GivenTheseExistingCars(Table table)
        {
            table.Rows.ForEach(values =>
            {
                CarRepository.RemoveByRegistration(values["Registration"]);

                uint customerId = CustomerRepository.GetFirstCustomerId();

                var car = new CarInfo(
                    values["Registration"],
                    customerId,
                    values["Make"],
                    values["Model"]);

                CarRepository.Create(car);
            });
        }

        [Given(@"there is no existing car with registration '(.*)'")]
        public void GivenThereIsNoExistingCarWithRegistration(string registration)
        {
            CarRepository.RemoveByRegistration(registration);
        }

        [When(@"I change the registration of '(.*)' to '(.*)'")]
        public void WhenIChangeTheRegistrationOfTo(string currentRegistration, string newRegistration)
        {
            _driver = AutoWorkshopDriver.CreateAuthenticatedInstance();
            _changeCarRegistrationPage = new ChangeCarRegistrationPage(_driver);

            _changeCarRegistrationPage.ChangeRegistration(currentRegistration, newRegistration);
        }

        [Then(@"I should see the success message '(.*)'")]
        public void ThenIShouldSeeTheSuccessMessage(string expectedMessage)
        {
            _changeCarRegistrationPage.Should().NotBeNull();

            string successMessage = _changeCarRegistrationPage.GetSuccessMessage();

            successMessage.Should().Be(expectedMessage);
        }

        [Then(@"I should see the error message '(.*)'")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            _changeCarRegistrationPage.Should().NotBeNull();

            string errorMessage = _changeCarRegistrationPage.GetErrorMessage();

            errorMessage.Should().Be(expectedMessage);
        }

        [Then(@"the following car should be present in the system")]
        [Then(@"the following cars should be present in the system")]
        public void ThenTheFollowingCarsShouldBePresentInTheSystem(Table table)
        {
            table.Rows.ForEach(expectedValues =>
            {
                CarInfo storedCar = CarRepository.GetInfoByRegistration(expectedValues["Registration"]);

                storedCar.Should().NotBeNull();
                storedCar.Registration.Should().Be(expectedValues["Registration"]);
                storedCar.Make.Should().Be(expectedValues["Make"]);
                storedCar.Model.Should().Be(expectedValues["Model"]);
            });
        }

        [Then(@"there should be no car with registration '(.*)'")]
        public void ThenThereShouldBeNoCarWithRegistration(string registration)
        {
            CarInfo storedCar = CarRepository.GetInfoByRegistration(registration);

            storedCar.Should().BeNull();
        }
    }
}
