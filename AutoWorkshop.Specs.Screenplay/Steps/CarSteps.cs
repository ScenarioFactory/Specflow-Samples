namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Abilities;
    using Actors;
    using Database.Questions;
    using Database.Tasks;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarSteps
    {
        private readonly Actor _actor;

        public CarSteps(AppSettings appSettings)
        {
            _actor = new Actor();
            _actor.Can(UseMySqlDatabase.With(appSettings.MySqlConnectionString));
        }

        [Given(@"this existing car")]
        [Given(@"these existing cars")]
        [Given(@"the following cars")]
        public void GivenTheseExistingCars(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _actor.AttemptsTo(DeleteCar.ByRegistration(values["Registration"]));

                int customerId = _actor.AsksFor(
                    values.ContainsKey("Customer") ? StoredCustomerId.ForName(values["Customer"]) : StoredCustomerId.First());

                _actor.AttemptsTo(
                    InsertCar.WithRegistration(values["Registration"])
                        .ForCustomer(customerId)
                        .WithMake(values["Make"])
                        .WithModel(values["Model"]));
            });
        }

        [Given(@"there is no existing car with registration '(.*)'")]
        public void GivenThereIsNoExistingCarWithRegistration(string registration)
        {
            _actor.AttemptsTo(DeleteCar.ByRegistration(registration));
        }

        [Then(@"the following car should be present in the system")]
        [Then(@"the following cars should be present in the system")]
        public void ThenTheFollowingCarsShouldBePresentInTheSystem(Table table)
        {
            table.Rows.ForEach(expectedValues =>
            {
                CarInfo storedCar = _actor.AsksFor(StoredCar.WithRegistration(expectedValues["Registration"]));

                storedCar.Should().NotBeNull();
                storedCar.Registration.Should().Be(expectedValues["Registration"]);
                storedCar.Make.Should().Be(expectedValues["Make"]);
                storedCar.Model.Should().Be(expectedValues["Model"]);
            });
        }

        [Then(@"there should be no car with registration '(.*)'")]
        public void ThenThereShouldBeNoCarWithRegistration(string registration)
        {
            CarInfo storedCar = _actor.AsksFor(StoredCar.WithRegistration(registration));

            storedCar.Should().BeNull();
        }
    }
}
