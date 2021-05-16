namespace AutoWorkshop.Specs.Screenplay.Database.Tasks
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class InsertCar : DatabaseTask
    {
        private readonly string _registration;
        private int _customerId;
        private string _make;
        private string _model;

        private InsertCar(string registration)
        {
            _registration = registration;
        }

        public static InsertCar WithRegistration(string registration)
        {
            return new InsertCar(registration);
        }

        public InsertCar ForCustomer(int customerId)
        {
            _customerId = customerId;
            return this;
        }

        public InsertCar WithMake(string make)
        {
            _make = make;
            return this;
        }

        public InsertCar WithModel(string model)
        {
            _model = model;
            return this;
        }

        protected override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute(@"
                INSERT INTO cars
                    (car_regis, car_custid, car_make, car_model)
                VALUES
                    (@registration, @customerId, @make, @model)",
                new
                {
                    registration = _registration,
                    customerId = _customerId,
                    make = _make,
                    model = _model
                });
        }
    }
}
