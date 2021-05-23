namespace AutoWorkshop.Specs.Rest.Database.Questions
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Screenplay;

    public class StoredCarExists : DatabaseQuestion<bool>
    {
        private readonly string _registration;

        private StoredCarExists(string registration)
        {
            _registration = registration;
        }

        public static StoredCarExists WithRegistration(string registration)
        {
            return new StoredCarExists(registration);
        }

        protected override bool AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            int totalRows = connection.ExecuteScalar<int>(@"
                SELECT
                    COUNT(*)
                FROM
                    cars
                WHERE
                    car_regis = @registration",
                new { registration = _registration });

            return totalRows > 0;
        }
    }
}
