namespace AutoWorkshop.Specs.Screenplay.Repositories
{
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;

    public class CustomerRepository
    {
        private readonly AppSettings _appSettings;

        public CustomerRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void RemoveByName(string name)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            connection.Execute("DELETE FROM customers WHERE cus_name = @name", new { name });
        }

        public CustomerInfo GetInfoByName(string name)
        {
            using var connection = new MySqlConnection(_appSettings.MySqlConnectionString);

            return connection.QuerySingle<CustomerInfo>(@"
                SELECT
                    cus_title title,
                    cus_name name,
                    cus_address1 addressLine1,
                    cus_address2 addressLine2,
                    cus_address3 addressLine3,
                    cus_postcode postcode,
                    cus_homephone homePhone,
                    cus_mobile mobile,
                    cus_accountinv accountInvoicing
                FROM
                    customers
                WHERE
                    cus_name = @name",
                new { name });
        }
    }
}
