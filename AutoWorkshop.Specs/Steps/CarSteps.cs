namespace AutoWorkshop.Specs.Steps
{
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Framework;
    using Repositories;
    using Services;
    using SharedKernel.Commands;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarSteps
    {
        private readonly CarRepository _carRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly JobRepository _jobRepository;
        private readonly MotReminderRepository _motReminderRepository;
        private readonly ServiceBus _serviceBus;
        private readonly BlobStorage _blobStorage;

        public CarSteps(
            CarRepository carRepository,
            CustomerRepository customerRepository,
            JobRepository jobRepository,
            MotReminderRepository motReminderRepository,
            ServiceBus serviceBus,
            BlobStorage blobStorage)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _jobRepository = jobRepository;
            _motReminderRepository = motReminderRepository;
            _serviceBus = serviceBus;
            _blobStorage = blobStorage;
        }

        [Given(@"this existing car")]
        [Given(@"these existing cars")]
        [Given(@"the following cars")]
        public void GivenTheseExistingCars(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _carRepository.RemoveByRegistration(values["Registration"]);

                int customerId = values.ContainsKey("Customer") ?
                    _customerRepository.GetIdByName(values["Customer"]) :
                    _customerRepository.GetFirstCustomerId();

                var car = new CarInfo(
                    values["Registration"],
                    customerId,
                    values["Make"],
                    values["Model"],
                    values.GetDateOrDefault("MOT Expiry"),
                    values.GetBoolOrDefault("Suppress MOT Reminder"));

                _carRepository.Create(car);
            });
        }

        [Given(@"there is no existing car with registration '(.*)'")]
        public void GivenThereIsNoExistingCarWithRegistration(string registration)
        {
            _carRepository.RemoveByRegistration(registration);
            _jobRepository.RemoveByRegistration(registration);
        }

        [Given(@"there have been no MOT Reminders issued")]
        public void GivenThereHaveBeenNoMotRemindersIssued()
        {
            _motReminderRepository.Clear();
        }

        [When(@"I issue MOT Reminders")]
        public async void WhenIIssueMotReminders()
        {
            var command = new InitiateMotReminderGeneration();
            await _serviceBus.SendAsync("cars.initiatemotremindergeneration", command);
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
                _motReminderRepository.GetByRegistration,
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

                    return _blobStorage.Exists(blobContainerName, blobFileName);
                }

                bool documentGenerated = Poller.PollForResult(IsDocumentInBlobStorage);

                documentGenerated.Should().BeTrue($"document {row["Document Name"]} should be present in blob storage");
            });
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