namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Abilities;
    using Actors;
    using Drivers;
    using FluentAssertions;
    using Pages;
    using Questions;
    using Tasks;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarUiSteps
    {
        private readonly Actor _actor;

        public CarUiSteps(AutoWorkshopDriver driver)
        {
            _actor = new Actor();
            _actor.Can(UseAutoWorkshop.With(driver));
        }

        [When(@"I change the registration of '(.*)' to '(.*)'")]
        public void WhenIChangeTheRegistrationOfTo(string currentRegistration, string newRegistration)
        {
            _actor.AttemptsTo(
                Navigate.ToChangeRegistration(),
                ChangeCarRegistration.From(currentRegistration).To(newRegistration));
        }

        [Then(@"I should see the success message '(.*)'")]
        public void ThenIShouldSeeTheSuccessMessage(string expectedMessage)
        {
            string successMessage = _actor.AsksFor(SuccessMessage.For(ChangeCarRegistrationPage.SuccessMessage));

            successMessage.Should().Be(expectedMessage);
        }

        [Then(@"I should see the error message '(.*)'")]
        public void ThenIShouldSeeTheErrorMessage(string expectedMessage)
        {
            string errorMessage = _actor.AsksFor(ErrorMessage.For(ChangeCarRegistrationPage.ErrorMessage));

            errorMessage.Should().Be(expectedMessage);
        }
    }
}
