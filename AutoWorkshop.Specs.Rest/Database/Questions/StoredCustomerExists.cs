namespace AutoWorkshop.Specs.Rest.Database.Questions
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Screenplay;

    public class StoredCustomerExists : DatabaseQuestion<bool>
    {
        private readonly int _customerId;

        private StoredCustomerExists(int customerId)
        {
            _customerId = customerId;
        }

        public static StoredCustomerExists WithId(int customerId)
        {
            return new StoredCustomerExists(customerId);
        }

        protected override bool AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            int totalRows = connection.ExecuteScalar<int>(@"
                SELECT
                    COUNT(*)
                FROM
                    customers
                WHERE
                    cus_custid = @customerId",
                new { customerId = (uint)_customerId });

            return totalRows > 0;
        }
    }
}
