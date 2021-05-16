namespace AutoWorkshop.Specs.Screenplay.Database.Tasks
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class DeleteMotReminders : DatabaseTask
    {
        private DeleteMotReminders()
        {
        }

        public static DeleteMotReminders ForAll()
        {
            return new DeleteMotReminders();
        }

        protected override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute("DELETE FROM motreminder");
        }
    }
}
