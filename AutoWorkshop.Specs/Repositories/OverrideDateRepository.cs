namespace AutoWorkshop.Specs.Repositories
{
    using System;
    using Dapper;
    using MySql.Data.MySqlClient;

    public class OverrideDateRepository
    {
        private readonly AppSettings _appSettings;

        public OverrideDateRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void SetCurrentDate(DateTime currentDate)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            connection.Execute("DELETE FROM overridedate");
            connection.Execute("INSERT INTO overridedate VALUES (@currentDate)", new { currentDate });
        }
    }
}
