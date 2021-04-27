namespace AutoWorkshop.Specs.Repositories
{
    using System.Linq;
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;

    public static class CustomerRepository
    {
        private static readonly string ConnectionString = Configuration.AppSettings["AutoWorkshop:MySqlConnectionString"];

        public static CustomerInfo GetInfoByName(string name)
        {
            using var connection = new MySqlConnection(ConnectionString);
            return connection.Query<CustomerInfo>(@"
                SELECT
                    cus_title title,
                    cus_name name,
                    cus_address1 addressLine1,
                    cus_address2 addressLine2,
                    cus_address3 addressLine3,
                    cus_postcode postcode,
                    cus_homephone homePhone,
                    cus_mobile mobile,
                    cus_accountinv isAccountInvoicing
                FROM
                    customers
                WHERE
                    cus_name = @name", new { name })
                .Single();
        }

        public static void RemoveByName(string name)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Execute("DELETE FROM customers WHERE cus_name = @name", new { name });
        }
    }
}
