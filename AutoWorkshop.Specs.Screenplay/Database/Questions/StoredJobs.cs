namespace AutoWorkshop.Specs.Screenplay.Database.Questions
{
    using System.Linq;
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class StoredJobs : DatabaseQuestion<JobInfo[]>
    {
        private readonly string _registration;

        private StoredJobs(string registration)
        {
            _registration = registration;
        }

        public static StoredJobs ForRegistration(string registration)
        {
            return new StoredJobs(registration);
        }

        public override JobInfo[] AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

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
                    new { registration = _registration })
                .ToArray();
        }
    }
}
