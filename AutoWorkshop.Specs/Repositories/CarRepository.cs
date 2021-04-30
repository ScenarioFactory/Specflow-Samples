namespace AutoWorkshop.Specs.Repositories
{
    using System.Linq;
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;

    public static class CarRepository
    {
        private static readonly string ConnectionString = Configuration.AppSettings["AutoWorkshop:MySqlConnectionString"];

        public static void Create(CarInfo car)
        {
            using var connection = new MySqlConnection(ConnectionString);

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

        public static void RemoveByRegistration(string registration)
        {
            using var connection = new MySqlConnection(ConnectionString);

            connection.Execute("DELETE FROM cars WHERE car_regis = @registration", new { registration });
        }

        public static CarInfo GetInfoByRegistration(string registration)
        {
            using var connection = new MySqlConnection(ConnectionString);

            return connection.Query<CarInfo>(@"
                SELECT
                    car_regis registration,
                    car_custid customerId,
                    car_make make,
                    car_model model
                FROM
                    cars
                WHERE
                    car_regis = @registration",
                    new { registration })
                .SingleOrDefault();
        }
    }
}
