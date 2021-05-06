namespace AutoWorkshop.Specs.Stateless.Repositories
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

        public void RemoveByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            connection.Execute("DELETE FROM cars WHERE car_regis = @registration", new { registration });
        }

        public CarInfo GetInfoByRegistration(string registration)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            return connection.QuerySingleOrDefault<CarInfo>(@"
                SELECT
                    car_regis registration,
                    car_custid customerId,
                    car_make make,
                    car_model model
                FROM
                    cars
                WHERE
                    car_regis = @registration",
                    new { registration });
        }
    }
}
