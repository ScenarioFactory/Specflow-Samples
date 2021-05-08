namespace AutoWorkshop.Specs.Steps
{
    using Dto;
    using Extensions;
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
    }
}