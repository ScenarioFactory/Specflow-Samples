namespace AutoWorkshop.Specs.Stateless.Extensions
{
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static int GetInt(this TableRow row, string column)
        {
            return int.Parse(row[column]);
        }
    }
}