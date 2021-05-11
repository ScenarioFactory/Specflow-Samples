namespace AutoWorkshop.Specs.Screenplay.Database.Questions
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class StoredCustomerId : DatabaseQuestion<int>
    {
        private readonly string _name;

        private StoredCustomerId(string name = null)
        {
            _name = name;
        }

        public static StoredCustomerId ForName(string name)
        {
            return new StoredCustomerId(name);
        }

        public static StoredCustomerId First()
        {
            return new StoredCustomerId();
        }

        public override int AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            return _name != null ?
                connection.ExecuteScalar<int>("SELECT cus_custid FROM customers WHERE cus_name = @name", new {name = _name}) :
                connection.ExecuteScalar<int>("SELECT cus_custid FROM customers ORDER BY cus_custid LIMIT 1");
        }
    }
}
