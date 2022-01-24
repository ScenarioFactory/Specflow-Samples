namespace AutoWorkshop.Specs.Steps
{
    using Dto;
    using Framework;
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

        [Given(@"the following customers")]
        public void GivenTheFollowingCustomers(Table table)
        {
            table.Rows.ForEach(values =>
            {
                _customerRepository.RemoveByName(values["Name"]);

                var customer = new CustomerInfo(
                    values["Title"],
                    values["Name"],
                    values["Address Line 1"],
                    values["Address Line 2"],
                    values["Address Line 3"],
                    values["Postcode"],
                    values["Home Phone"],
                    values["Mobile"],
                    1);

                _customerRepository.Create(customer);
            });
        }
    }
}