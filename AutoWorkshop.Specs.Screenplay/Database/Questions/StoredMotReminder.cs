namespace AutoWorkshop.Specs.Screenplay.Database.Questions
{
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class StoredMotReminder : DatabaseQuestion<MotReminderInfo>
    {
        private readonly string _registration;

        private StoredMotReminder(string registration)
        {
            _registration = registration;
        }

        public static StoredMotReminder ForRegistration(string registration)
        {
            return new StoredMotReminder(registration);
        }

        public override MotReminderInfo AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

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
                new { registration = _registration });
        }
    }
}
