namespace AutoWorkshop.Specs.Rest.Steps
{
    using System.Linq;
    using Database;
    using Database.Questions;
    using Database.Tasks;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Rest;
    using Rest.Interactions;
    using RestSharp;
    using Screenplay;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarSteps
    {
        private readonly RestResponseInfo _lastResponse;
        private readonly IActor _actor;
        private CarInfo _newCarInput;
        private CarInfo _storedCar;
        private CarInfo _updatedCarInput;

        public CarSteps(AppSettings appSettings, RestResponseInfo lastResponse)
        {
            _lastResponse = lastResponse;
            _actor = new Actor().WhoCan(
                UseMySqlDatabase.With(appSettings.MySqlConnectionString),
                CallRestApi.Using(new RestClient(appSettings.RestApiUrl)));
        }

        [Given(@"there is no car with registration '(.*)'")]
        public void GivenThereIsNoCarWithRegistration(string registration)
        {
            _actor.AttemptsTo(DeleteCar.WithRegistration(registration));
        }

        [Given(@"the following existing car")]
        public void GivenTheFollowingExistingCar(Table table)
        {
            var values = table.Rows.Single();

            _actor.AttemptsTo(DeleteCar.WithRegistration(values["Registration"]));

            int customerId = _actor.AsksFor(StoredCustomerId.ForName(values["Customer"]));

            _actor.AttemptsTo(
                InsertCar.WithRegistration(values["Registration"])
                    .ForCustomer(customerId)
                    .WithMake(values.GetStringOrDefault("Make"))
                    .WithModel(values.GetStringOrDefault("Model"))
                    .MotExpiringOn(values.GetDateOrDefault("MOT Expiry"))
                    .SuppressingMotReminder(values.GetBoolOrDefault("Suppress MOT Reminder")));

            _storedCar = _actor.AsksFor(StoredCar.WithRegistration(values["Registration"]));
        }

        [When(@"I create a new car resource with the following details")]
        public void WhenICreateANewCarResourceWithTheFollowingDetails(Table table)
        {
            var values = table.Rows.Single();

            int customerId = _actor.AsksFor(StoredCustomerId.ForName(values["Customer"]));

            _newCarInput = new CarInfo
            {
                Registration = values["Registration"],
                CustomerId = customerId,
                Make = values["Make"],
                Model = values["Model"],
                MotExpiry = values.GetDateOrDefault("MOT Expiry"),
                SuppressMotReminder = values.GetBool("Suppress MOT Reminder")
            };

            _lastResponse.Response = _actor.Calls(Post.Resource(_newCarInput).To("api/car"));
        }

        [When(@"I request the car resource")]
        public void WhenIRequestTheCarResource()
        {
            _storedCar.Should().NotBeNull();

            _lastResponse.Response = _actor.Calls(Get.ResourceAt($"api/car/{_storedCar.Registration}"));
        }

        [When(@"I request a car resource with registration (.*)")]
        public void WhenIRequestACarResourceWithRegistration(string registration)
        {
            _lastResponse.Response = _actor.Calls(Get.ResourceAt($"api/car/{registration}"));
        }

        [When(@"I update the car resource with the following changes")]
        public void WhenIUpdateTheCarResourceWithTheFollowingChanges(Table table)
        {
            _storedCar.Should().NotBeNull();

            var values = table.Rows.Single();

            _updatedCarInput = new CarInfo
            {
                Registration = _storedCar.Registration,
                CustomerId = _storedCar.CustomerId,
                Make = _storedCar.Make,
                Model = values["Model"],
                MotExpiry = values.GetDateOrDefault("MOT Expiry"),
                SuppressMotReminder = _storedCar.SuppressMotReminder
            };

            _lastResponse.Response = _actor.Calls(Put.Resource(_updatedCarInput).At($"api/car/{_updatedCarInput.Registration}"));
        }

        [When(@"I delete the car resource")]
        public void WhenIDeleteTheCarResource()
        {
            _storedCar.Should().NotBeNull();

            _lastResponse.Response = _actor.Calls(Delete.ResourceAt($"api/car/{_storedCar.Registration}"));
        }

        [Then(@"the car should be added to the system with the details provided")]
        public void ThenTheCarShouldBeAddedToTheSystemWithTheDetailsProvided()
        {
            _newCarInput.Should().NotBeNull();

            CarInfo storedCar = _actor.AsksFor(StoredCar.WithRegistration(_newCarInput.Registration));

            storedCar.CustomerId.Should().Be(_newCarInput.CustomerId);
            storedCar.Make.Should().Be(_newCarInput.Make);
            storedCar.Model.Should().Be(_newCarInput.Model);
            storedCar.MotExpiry.Should().Be(_newCarInput.MotExpiry);
            storedCar.SuppressMotReminder.Should().Be(_newCarInput.SuppressMotReminder);
        }

        [Then(@"I should receive the full details of the car")]
        public void ThenIShouldReceiveTheFullDetailsOfTheCar()
        {
            _lastResponse.Response.Should().NotBeNull();
            _storedCar.Should().NotBeNull();

            CarInfo responseCar = _lastResponse.Response.Content.FromJson<CarInfo>();

            responseCar.CustomerId.Should().Be(_storedCar.CustomerId);
            responseCar.Make.Should().Be(_storedCar.Make);
            responseCar.Model.Should().Be(_storedCar.Model);
            responseCar.MotExpiry.Should().Be(_storedCar.MotExpiry);
            responseCar.SuppressMotReminder.Should().Be(_storedCar.SuppressMotReminder);
        }

        [Then(@"the changes should be made to the car in the system")]
        public void ThenTheChangesShouldBeMadeToTheCarInTheSystem()
        {
            _updatedCarInput.Should().NotBeNull();

            CarInfo storedCar = _actor.AsksFor(StoredCar.WithRegistration(_updatedCarInput.Registration));

            storedCar.Registration.Should().Be(_updatedCarInput.Registration);
            storedCar.Make.Should().Be(_updatedCarInput.Make);
            storedCar.Model.Should().Be(_updatedCarInput.Model);
            storedCar.MotExpiry.Should().Be(_updatedCarInput.MotExpiry);
            storedCar.SuppressMotReminder.Should().Be(_updatedCarInput.SuppressMotReminder);
        }

        [Then(@"the car should be removed from the system")]
        public void ThenTheCarShouldBeRemovedFromTheSystem()
        {
            _storedCar.Should().NotBeNull();

            bool carPresentInStorage = _actor.AsksFor(StoredCarExists.WithRegistration(_storedCar.Registration));
            carPresentInStorage.Should().BeFalse();
        }

        [Then(@"the car should remain unchanged in the system")]
        public void ThenTheCarShouldRemainUnchangedInTheSystem()
        {
            _storedCar.Should().NotBeNull();

            CarInfo latestStoredCar = _actor.AsksFor(StoredCar.WithRegistration(_newCarInput.Registration));

            latestStoredCar.Registration.Should().Be(_storedCar.Registration);
            latestStoredCar.CustomerId.Should().Be(_storedCar.CustomerId);
            latestStoredCar.Make.Should().Be(_storedCar.Make);
            latestStoredCar.Model.Should().Be(_storedCar.Model);
            latestStoredCar.MotExpiry.Should().Be(_storedCar.MotExpiry);
            latestStoredCar.SuppressMotReminder.Should().Be(_storedCar.SuppressMotReminder);
        }
    }
}
