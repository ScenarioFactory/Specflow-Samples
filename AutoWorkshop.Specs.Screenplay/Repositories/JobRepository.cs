namespace AutoWorkshop.Specs.Screenplay.Repositories
{
    using Dapper;
    using MySql.Data.MySqlClient;

    public class JobRepository
    {
        private readonly AppSettings _appSettings;

        public JobRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void RemoveByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute("DELETE FROM jobs WHERE job_regis = @registration", new { registration });
        }
    }
}
