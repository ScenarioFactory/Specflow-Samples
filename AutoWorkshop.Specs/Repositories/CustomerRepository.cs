namespace AutoWorkshop.Specs.Repositories
{
    using System.Linq;
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;

    public static class CustomerRepository
    {
        private static readonly string ConnectionString = Configuration.AppSettings["AutoWorkshop:MySqlConnectionString"];

        public static void Create(CustomerInfo customer)
        {
            using var connection = new MySqlConnection(ConnectionString);

            connection.Execute(@"
                INSERT INTO customers
                    (cus_title, cus_name, cus_address1, cus_address2, cus_address3, cus_postcode, cus_homephone, cus_mobile, cus_accountinv)
                VALUES
                    (@title, @name, @addressLine1, @addressLine2, @addressLine3, @postcode, @homephone, @mobile, @accountInvoicing)",
                new {customer.Title, customer.Name, customer.AddressLine1, customer.AddressLine2, customer.AddressLine3, customer.Postcode,
                    customer.HomePhone, customer.Mobile, customer.AccountInvoicing});
        }

        public static int GetIdByName(string name)
        {
            using var connection = new MySqlConnection(ConnectionString);

            return connection.Query<int>(@"
                SELECT
                    cus_custid
                FROM
                    customers
                WHERE
                    cus_name = @name",
                    new { name })
                .Single();
        }

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
                    cus_accountinv accountInvoicing
                FROM
                    customers
                WHERE
                    cus_name = @name",
                    new { name })
                .Single();
        }

        public static void RemoveByName(string name)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Execute("DELETE FROM customers WHERE cus_name = @name", new { name });
        }
    }
}
