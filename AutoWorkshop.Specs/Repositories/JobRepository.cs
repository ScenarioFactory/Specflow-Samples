namespace AutoWorkshop.Specs.Repositories
{
    using System.Linq;
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;

    public class JobRepository
    {
        private readonly AppSettings _appSettings;

        public JobRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public JobInfo[] GetByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            return connection.Query<JobInfo>(@"
                SELECT
                    job_regis registration,
                    job_description description,
                    job_start date,
                    job_hours hours,
                    job_mileage mileage
                FROM
                    jobs
                WHERE
                    job_regis = @registration",
                    new { registration })
                .ToArray();
        }

        public void RemoveByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute("DELETE FROM jobs WHERE job_regis = @registration", new { registration });
        }
    }
}