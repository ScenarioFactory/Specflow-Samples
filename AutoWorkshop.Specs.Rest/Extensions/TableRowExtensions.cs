namespace AutoWorkshop.Specs.Rest.Extensions
{
    using System;
    using System.Globalization;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static bool GetBool(this TableRow row, string column)
        {
            return row[column] switch
            {
                "Yes" => true,
                "No" => false,
                _ => bool.Parse(row[column])
            };
        }

        public static bool GetBoolOrDefault(this TableRow row, string column)
        {
            return row.ContainsKey(column) && row.GetBool(column);
        }

        public static DateTime GetDate(this TableRow row, string column)
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(row[column], "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static DateTime? GetDateOrDefault(this TableRow row, string column)
        {
            if (row.ContainsKey(column))
            {
                return row.GetDate(column);
            }

            return null;
        }

        public static string GetStringOrDefault(this TableRow row, string column)
        {
            if (row.ContainsKey(column))
            {
                return row[column];
            }

            return null;
        }
    }
}
