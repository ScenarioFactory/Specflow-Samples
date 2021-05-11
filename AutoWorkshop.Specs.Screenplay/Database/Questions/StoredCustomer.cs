namespace AutoWorkshop.Specs.Screenplay.Database.Questions
{
    using Dapper;
    using Dto;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class StoredCustomer : DatabaseQuestion<CustomerInfo>
    {
        private readonly string _name;

        private StoredCustomer(string name)
        {
            _name = name;
        }

        public static StoredCustomer WithName(string name)
        {
            return new StoredCustomer(name);
        }

        public override CustomerInfo AskAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

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
                new { name = _name });
        }
    }
}
