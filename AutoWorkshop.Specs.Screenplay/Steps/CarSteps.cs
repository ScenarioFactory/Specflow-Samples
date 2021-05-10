namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Dto;
    using Extensions;
    using FluentAssertions;
    using Repositories;
    using TechTalk.SpecFlow;

    [Binding]
    public class CarSteps
    {
        private readonly CarRepository _carRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly JobRepository _jobRepository;

        public CarSteps(CarRepository carRepository, CustomerRepository customerRepository, JobRepository jobRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _jobRepository = jobRepository;
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
