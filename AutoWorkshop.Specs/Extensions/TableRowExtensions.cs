namespace AutoWorkshop.Specs.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static bool ParseBoolean(this TableRow row, string column)
        {
            return bool.Parse(row[column]);
        }

        public static DateTime ParseDate(this TableRow row, string column, string format = "dd/MM/yyyy")
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(row[column], format, CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static decimal ParseDecimal(this TableRow row, string column)
        {
            return decimal.Parse(row[column]);
        }

        public static int ParseInt(this TableRow row, string column)
        {
            return int.Parse(row[column]);
        }

        public static void ForEach(this IEnumerable<TableRow> rows, Action<TableRow> action)
        {
            foreach (var row in rows)
            {
                action(row);
            }
        }
    }
}