namespace AutoWorkshop.Specs.Screenplay.Steps
{
    using Repositories;
    using TechTalk.SpecFlow;

    [Binding]
    public class CustomerSteps
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerSteps(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [Given(@"there are no customers named '(.*)'")]
        public void GivenThereAreNoCustomersNamed(string customerName)
        {
            _customerRepository.RemoveByName(customerName);
        }
    }
}