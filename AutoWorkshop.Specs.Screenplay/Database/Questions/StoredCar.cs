namespace AutoWorkshop.Specs.Screenplay.Database.Questions
{
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class StoredCar : DatabaseQuestion<CarInfo>
    {
        private readonly string _registration;

        private StoredCar(string registration)
        {
            _registration = registration;
        }

        public static StoredCar WithRegistration(string registration)
        {
            return new StoredCar(registration);
        }

        public override CarInfo AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            return connection.QuerySingleOrDefault<CarInfo>(@"
                SELECT
                    car_regis registration,
                    car_custid customerId,
                    car_make make,
                    car_model model,
                    DATE_FORMAT(car_motexpiry, '%Y-%m-%d') motExpiry,
                    car_motsuppress suppressMotReminder
                FROM
                    cars
                WHERE
                    car_regis = @registration",
                new { registration = _registration });
        }
    }
}
