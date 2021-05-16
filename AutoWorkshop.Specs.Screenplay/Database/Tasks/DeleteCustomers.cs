namespace AutoWorkshop.Specs.Screenplay.Database.Tasks
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class DeleteCustomers : DatabaseTask
    {
        private readonly string _name;

        private DeleteCustomers(string name)
        {
            _name = name;
        }

        public static DeleteCustomers WithName(string name)
        {
            return new DeleteCustomers(name);
        }

        protected override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute("DELETE FROM customers WHERE cus_name = @name", new { name = _name });
        }
    }
}
