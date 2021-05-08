namespace AutoWorkshop.Specs.Steps
{
    using System.Linq;
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Repositories;
    using Services;
    using SharedKernel.Commands;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarSteps
    {
        private readonly CarRepository _carRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly MotReminderRepository _motReminderRepository;
        private readonly ServiceBus _serviceBus;

        public CarSteps(CarRepository carRepository, CustomerRepository customerRepository, MotReminderRepository motReminderRepository, ServiceBus serviceBus)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _motReminderRepository = motReminderRepository;
            _serviceBus = serviceBus;
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
    }
}