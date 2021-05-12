namespace AutoWorkshop.Specs.Screenplay.Database.Tasks
{
    using System;
    using Dapper;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class SetCurrentDate : DatabaseTask
    {
        private readonly DateTime _currentDate;

        private SetCurrentDate(DateTime currentDate)
        {
            _currentDate = currentDate.Date;
        }

        public static SetCurrentDate To(DateTime currentDate)
        {
            return new SetCurrentDate(currentDate);
        }

        public override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute("DELETE FROM overridedate");
            connection.Execute("INSERT INTO overridedate VALUES (@currentDate)", new { currentDate = _currentDate });
        }
    }
}
