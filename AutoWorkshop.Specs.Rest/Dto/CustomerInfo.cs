namespace AutoWorkshop.Specs.Rest.Dto
{
    public class CustomerInfo
    {
        public CustomerInfo()
        {
        }

        /// <summary>
        /// Constructor used by Dapper.
        /// </summary>
        public CustomerInfo(
            uint customerId,
            string title,
            string name,
            string addressLine1,
            string addressLine2,
            string addressLine3,
            string postcode,
            string homePhone,
            string mobile,
            byte accountInvoicing)
        {
            CustomerId = (int)customerId;
            Title = title;
            Name = name;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            Postcode = postcode;
            HomePhone = homePhone;
            Mobile = mobile;
            HasAccountInvoicing = accountInvoicing == 1;
        }

        public int CustomerId { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string Postcode { get; set; }

        public string HomePhone { get; set; }

        public string Mobile { get; set; }

        public bool HasAccountInvoicing { get; set; }
    }
}
