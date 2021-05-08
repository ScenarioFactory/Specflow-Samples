namespace AutoWorkshop.Specs.Repositories
{
    using Dapper;
    using MySql.Data.MySqlClient;

    public class MotReminderRepository
    {
        private readonly AppSettings _appSettings;

        public MotReminderRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Clear()
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute("DELETE FROM motreminders");
        }
    }
}
