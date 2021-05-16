namespace AutoWorkshop.Specs.Screenplay.WebDriver.Tasks
{
    using Pages;
    using Pattern;

    public class ChangeCarRegistration : WebTask
    {
        private readonly string _currentRegistration;
        private string _newRegistration;

        private ChangeCarRegistration(string currentRegistration)
        {
            _currentRegistration = currentRegistration;
        }

        public static ChangeCarRegistration From(string currentRegistration)
        {
            return new ChangeCarRegistration(currentRegistration);
        }

        public ChangeCarRegistration To(string newRegistration)
        {
            _newRegistration = newRegistration;
            return this;
        }

        protected override void PerformAs(IActor actor, AutoWorkshopDriver driver)
        {
            actor.AttemptsTo(
                SendKeys.To(ChangeCarRegistrationPage.CurrentRegistration, _currentRegistration),
                SendKeys.To(ChangeCarRegistrationPage.NewRegistration, _newRegistration),
                Click.On(ChangeCarRegistrationPage.UpdateRegistration),
                AcceptAlert.WithText($"Change registration {_currentRegistration} to registration {_newRegistration}?"));
        }
    }
}