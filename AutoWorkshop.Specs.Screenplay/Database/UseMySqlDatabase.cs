namespace AutoWorkshop.Specs.Screenplay.Database
{
    using Pattern;

    public class UseMySqlDatabase : IAbility
    {
        private UseMySqlDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public static UseMySqlDatabase With(string connectionString)
        {
            return new UseMySqlDatabase(connectionString);
        }
    }
}