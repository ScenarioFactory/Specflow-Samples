namespace AutoWorkshop.Specs.Repositories
{
    using Dapper;
    using Dto;
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

            connection.Execute("DELETE FROM motreminder");
        }

        public MotReminderInfo GetByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            return connection.QuerySingleOrDefault<MotReminderInfo>(@"
                SELECT
                    car_regis registration,
                    DATE_FORMAT(car_motexpiry, '%Y-%m-%d') motExpiry,
                    car_make make,
                    car_model model,
                    cus_title title,
                    cus_name name,
                    cus_address1 addressLine1,
                    cus_address2 addressLine2,
                    cus_address3 addressLine3,
                    cus_postcode postcode
                FROM
                    motreminders
                WHERE
                    car_regis = @registration",
                new { registration });
        }
    }
}
