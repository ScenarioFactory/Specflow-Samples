namespace AutoWorkshop.Model
{
    public class InvoiceCosts
    {
        public InvoiceCosts(decimal labour, decimal parts, decimal vat, decimal mot, decimal total)
        {
            Labour = labour;
            Parts = parts;
            Vat = vat;
            Mot = mot;
            Total = total;
        }

        public decimal Labour { get; }

        public decimal Parts { get; }

        public decimal Vat { get; }

        public decimal Mot { get; }

        public decimal Total { get; }
    }
}
