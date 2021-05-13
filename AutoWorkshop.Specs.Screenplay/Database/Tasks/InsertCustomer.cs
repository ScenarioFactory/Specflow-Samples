namespace AutoWorkshop.Specs.Screenplay.Database.Tasks
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using Pattern;

    public class InsertCustomer : DatabaseTask
    {
        private readonly string _name;
        private string _title;
        private string _addressLine1;
        private string _addressLine2;
        private string _addressLine3;
        private string _postcode;
        private string _homePhone;
        private string _mobile;

        private InsertCustomer(string name)
        {
            _name = name;
        }

        public static InsertCustomer Named(string name)
        {
            return new InsertCustomer(name);
        }

        public InsertCustomer Titled(string title)
        {
            _title = title;
            return this;
        }

        public InsertCustomer OfAddress(string addressLine1, string addressLine2, string addressLine3, string postcode)
        {
            _addressLine1 = addressLine1;
            _addressLine2 = addressLine2;
            _addressLine3 = addressLine3;
            _postcode = postcode;
            return this;
        }

        public InsertCustomer WithHomePhone(string homePhone)
        {
            _homePhone = homePhone;
            return this;
        }

        public InsertCustomer WithMobile(string mobile)
        {
            _mobile = mobile;
            return this;
        }

        public override void PerformAs(IActor actor, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);

            connection.Execute(@"
                INSERT INTO customers
                    (cus_title, cus_name, cus_address1, cus_address2, cus_address3, cus_postcode, cus_homephone, cus_mobile)
                VALUES
                    (@title, @name, @addressLine1, @addressLine2, @addressLine3, @postcode, @homephone, @mobile)",
                new
                {
                    title = _title,
                    name = _name,
                    addressLine1 = _addressLine1,
                    addressLine2 = _addressLine2,
                    addressLine3 = _addressLine3,
                    postcode = _postcode,
                    homePhone = _homePhone,
                    mobile = _mobile
                });
        }
    }
}
