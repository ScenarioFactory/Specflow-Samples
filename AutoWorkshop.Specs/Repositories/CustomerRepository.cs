namespace AutoWorkshop.Specs.Repositories
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

        public void Create(CustomerInfo customer)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            connection.Execute(@"
                INSERT INTO customers
                    (cus_title, cus_name, cus_address1, cus_address2, cus_address3, cus_postcode, cus_homephone, cus_mobile, cus_accountinv)
                VALUES
                    (@title, @name, @addressLine1, @addressLine2, @addressLine3, @postcode, @homephone, @mobile, @accountInvoicing)",
                new {customer.Title, customer.Name, customer.AddressLine1, customer.AddressLine2, customer.AddressLine3, customer.Postcode,
                    customer.HomePhone, customer.Mobile, customer.AccountInvoicing});
        }

        public int GetIdByName(string name)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            return connection.ExecuteScalar<int>("SELECT cus_custid FROM customers WHERE cus_name = @name", new { name });
        }

        public CustomerInfo GetInfoByName(string name)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

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

        public void RemoveByName(string name)
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            connection.Execute("DELETE FROM customers WHERE cus_name = @name", new { name });
        }

        public int GetFirstCustomerId()
        {
            using var connection = new MySqlConnection(_appSettings.ConnectionString);

            return connection.ExecuteScalar<int>("SELECT cus_custid FROM customers ORDER BY cus_custid LIMIT 1");
        }
    }
}
