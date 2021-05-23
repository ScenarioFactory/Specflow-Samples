namespace AutoWorkshop.Specs.Rest.Database.Questions
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Screenplay;

    public class StoredCustomerId : DatabaseQuestion<int>
    {
        private readonly string _name;

        private StoredCustomerId(string name)
        {
            _name = name;
        }

        public static StoredCustomerId ForName(string name)
        {
            return new StoredCustomerId(name);
        }

        protected override int AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            return connection.ExecuteScalar<int>("SELECT cus_custid FROM customers WHERE cus_name = @name", new {name = _name});
        }
    }
}
