namespace AutoWorkshop.Specs.Rest.Database.Tasks
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Screenplay;

    public class DeleteCar : DatabaseTask
    {
        private readonly string _registration;

        private DeleteCar(string registration)
        {
            _registration = registration;
        }

        public static DeleteCar WithRegistration(string registration)
        {
            return new DeleteCar(registration);
        }

        protected override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute("DELETE FROM cars WHERE car_regis = @registration", new { registration = _registration});
            connection.Execute("DELETE FROM jobs WHERE job_regis = @registration", new { registration = _registration });
        }
    }
}
