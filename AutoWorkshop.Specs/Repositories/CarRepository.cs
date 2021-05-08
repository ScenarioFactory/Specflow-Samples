namespace AutoWorkshop.Specs.Repositories
{
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;

    public class CarRepository
    {
        private readonly AppSettings _appSettings;

        public CarRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Create(CarInfo car)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute(@"
                INSERT INTO cars
                    (car_regis, car_custid, car_make, car_model)
                VALUES
                    (@registration, @customerId, @make, @model)",
                new
                {
                    car.Registration,
                    car.CustomerId,
                    car.Make,
                    car.Model
                });
        }

        public void RemoveByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute("DELETE FROM cars WHERE car_regis = @registration", new { registration });
        }

        public CarInfo GetInfoByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

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
                    new { registration });
        }
    }
}
