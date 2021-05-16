namespace AutoWorkshop.Specs.Rest.Database.Tasks
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Screenplay;

    public class DeleteCustomer : DatabaseTask
    {
        private readonly int _customerId;

        private DeleteCustomer(int customerId)
        {
            _customerId = customerId;
        }

        public static DeleteCustomer WithId(int customerId)
        {
            return new DeleteCustomer(customerId);
        }

        protected override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute("DELETE FROM customers WHERE cus_custid = @customerId", new { customerId = _customerId });
        }
    }
}
