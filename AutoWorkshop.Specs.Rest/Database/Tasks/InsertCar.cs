namespace AutoWorkshop.Specs.Rest.Database.Tasks
{
    using System;
    using Dapper;
    using Extensions;
    using MySql.Data.MySqlClient;
    using Screenplay;

    public class InsertCar : DatabaseTask
    {
        private readonly string _registration;
        private int _customerId;
        private string _make;
        private string _model;
        private DateTime? _motExpiry;
        private bool _suppressMotReminder;

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

        public InsertCar MotExpiringOn(DateTime? motExpiry)
        {
            _motExpiry = motExpiry;
            return this;
        }

        public InsertCar SuppressingMotReminder(bool suppressMotReminder)
        {
            _suppressMotReminder = suppressMotReminder;
            return this;
        }

        protected override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute(@"
                INSERT INTO cars
                    (car_regis, car_custid, car_make, car_model, car_motexpiry, car_motsuppress)
                VALUES
                    (@registration, @customerId, @make, @model, @motExpiry, @suppressMotReminder)",
                new
                {
                    registration = _registration,
                    customerId = _customerId,
                    make = _make,
                    model = _model,
                    motExpiry = _motExpiry.ToMySqlDate(),
                    suppressMotReminder = _suppressMotReminder ? 1 : 0
                });
        }
    }
}
