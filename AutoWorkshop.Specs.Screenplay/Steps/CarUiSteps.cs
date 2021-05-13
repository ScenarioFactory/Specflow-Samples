namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using System.Linq;
    using Database;
    using Database.Questions;
    using Dto;
    using FluentAssertions;
    using Pages;
    using Pattern;
    using TechTalk.SpecFlow;
    using WebDriver;
    using WebDriver.Questions;
    using WebDriver.Tasks;

    [Binding]
    public class CarUiSteps
    {
        private readonly IActor _actor;
        private CarUiViewInfo _uiViewInfo;

        public CarUiSteps(AppSettings appSettings, AutoWorkshopDriver driver)
        {
            _actor = new Actor().WhoCan(
                UseAutoWorkshop.With(driver),
                UseMySqlDatabase.With(appSettings.MySqlConnectionString));
        }

        [When(@"I change the registration of '(.*)' to '(.*)'")]
        public void WhenIChangeTheRegistrationOfTo(string currentRegistration, string newRegistration)
        {
            _actor.AttemptsTo(
                Navigate.To(ChangeCarRegistrationPage.Path),
                ChangeCarRegistration.From(currentRegistration).To(newRegistration));
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

            _actor.AttemptsTo(
                CreateCar.WithRegistration(_uiViewInfo.Registration)
                    .AndMake(_uiViewInfo.Make)
                    .AndModel(_uiViewInfo.Model)
                    .ForYear(_uiViewInfo.Year));
        }

        [When(@"I select the option to create a new job for the car")]
        public void WhenISelectTheOptionToCreateANewJobForTheCar()
        {
            _actor.AttemptsTo(ClickToolbarButton.WithAltText("Add a new job"));
        }

        [Then(@"I should see the success message '(.*)'")]
        public void ThenIShouldSeeTheSuccessMessage(string expectedMessage)
        {
            string successMessage = _actor.AsksFor(Text.Of(ChangeCarRegistrationPage.SuccessMessage));

            successMessage.Should().Be(expectedMessage);
        }

        [Then(@"I should see the error message '(.*)'")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            string errorMessage = _actor.AsksFor(Text.Of(ChangeCarRegistrationPage.ErrorMessage));

            errorMessage.Should().Be(expectedMessage);
        }

        [Then(@"the car is added to the system with the details provided")]
        public void ThenTheCarIsAddedToTheSystemWithTheDetailsProvided()
        {
            _uiViewInfo.Should().NotBeNull();

            CarInfo storedCar = _actor.AsksFor(StoredCar.WithRegistration(_uiViewInfo.Registration));

            storedCar.Registration.Should().Be(_uiViewInfo.Registration);
            storedCar.Make.Should().Be(_uiViewInfo.Make);
            storedCar.Model.Should().Be(_uiViewInfo.Model);
        }
    }
}
