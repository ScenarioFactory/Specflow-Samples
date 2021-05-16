namespace AutoWorkshop.Specs.Rest.Extensions
{
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static bool GetBoolOrDefault(this TableRow row, string column)
        {
            if (!row.ContainsKey(column))
            {
                return false;
            }

            return row[column] switch
            {
                "Yes" => true,
                "No" => false,
                _ => bool.Parse(row[column])
            };
        }
    }
}
