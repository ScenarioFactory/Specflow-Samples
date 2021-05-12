namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using BlobStorage;
    using BlobStorage.Questions;
    using Database;
    using Database.Questions;
    using Database.Tasks;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Framework;
    using Pattern;
    using ServiceBus;
    using ServiceBus.Tasks;
    using SharedKernel.Commands;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarSteps
    {
        private readonly IActor _actor;

        public CarSteps(AppSettings appSettings)
        {
            _actor = new Actor().WhoCan(
                UseMySqlDatabase.With(appSettings.MySqlConnectionString),
                UseServiceBus.With(appSettings.ServiceBusConnectionString),
                UseBlobStorage.With(appSettings.BlobStorageConnectionString));
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

        [Given(@"there have been no MOT Reminders issued")]
        public void GivenThereHaveBeenNoMotRemindersIssued()
        {
            _actor.AttemptsTo(DeleteMotReminders.ForAll());
        }

        [When(@"I issue MOT Reminders")]
        public void WhenIIssueMotReminders()
        {
            _actor.AttemptsTo(SendCommand.Of(new InitiateMotReminderGeneration()).To("cars.initiatemotremindergeneration"));
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

        [Then(@"the following MOT Reminders should be issued")]
        public void ThenTheFollowingMotRemindersShouldBeIssued(Table table)
        {
            static bool IsMatchingRow(MotReminderInfo actual, TableRow expected)
            {
                return
                    actual.Registration == expected["Registration"] &&
                    actual.MotExpiry == expected.GetDate("MOT Expiry") &&
                    actual.Make == expected["Make"] &&
                    actual.Model == expected["Model"] &&
                    actual.Title == expected["Title"] &&
                    actual.Name == expected["Name"] &&
                    actual.AddressLine1 == expected["Address Line 1"] &&
                    actual.AddressLine2 == expected["Address Line 2"] &&
                    actual.AddressLine3 == expected["Address Line 3"] &&
                    actual.Postcode == expected["Postcode"];
            }

            var unmatchedRows = table.Rows.PollForUnmatchedRows(
                registration => _actor.AsksFor(StoredMotReminder.ForRegistration(registration)),
                row => row["Registration"],
                IsMatchingRow);

            unmatchedRows.Should().BeEmpty();
        }

        [Then(@"the following MOT Reminder documents have been generated")]
        public void ThenTheFollowingMotReminderDocumentsHaveBeenGenerated(Table table)
        {
            table.Rows.ForEach(row =>
            {
                bool IsDocumentInBlobStorage()
                {
                    const string blobContainerName = "motreminders";
                    string blobFileName = row["Document Name"];

                    return _actor.AsksFor(ExistenceOfBlob.WithName(blobFileName).InContainer(blobContainerName));
                }

                bool documentGenerated = Poller.PollForResult(IsDocumentInBlobStorage);

                documentGenerated.Should().BeTrue($"document {row["Document Name"]} should be present in blob storage");
            });
        }
    }
}
