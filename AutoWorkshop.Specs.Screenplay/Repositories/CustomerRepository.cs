namespace AutoWorkshop.Specs.Screenplay.Repositories
{
    using Dapper;
    using MySql.Data.MySqlClient;

    public class CustomerRepository
    {
        private readonly AppSettings _appSettings;

        public CustomerRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void RemoveByName(string name)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute("DELETE FROM customers WHERE cus_name = @name", new { name });
        }
    }
}
