namespace AutoWorkshop.Specs.Screenplay.Abilities
{
    using Pattern;

    public class UseServiceBus : IAbility
    {
        public string ConnectionString { get; }

        private UseServiceBus(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static UseServiceBus With(string connectionString)
        {
            return new UseServiceBus(connectionString);
        }
    }
}