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
    public class CarUiSteps
    {
        private readonly CarMaintenancePage _carMaintenancePage;
        private readonly ChangeCarRegistrationPage _changeCarRegistrationPage;
        private readonly CarRepository _carRepository;
        private CarUiViewInfo _uiViewInfo;

        public CarUiSteps(
            CarMaintenancePage carMaintenancePage,
            ChangeCarRegistrationPage changeCarRegistrationPage,
            CarRepository carRepository)
        {
            _carMaintenancePage = carMaintenancePage;
            _changeCarRegistrationPage = changeCarRegistrationPage;
            _carRepository = carRepository;
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

        [Then(@"the car should be added to the system with the details provided")]
        public void ThenTheCarShouldBeAddedToTheSystemWithTheDetailsProvided()
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
    }
}
