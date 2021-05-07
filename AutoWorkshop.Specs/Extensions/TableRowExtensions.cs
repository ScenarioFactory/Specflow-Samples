namespace AutoWorkshop.Specs.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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

        public static DateTime GetDate(this TableRow row, string column)
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(row[column], "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static DateTime? GetDateOrDefault(this TableRow row, string column, string format = "dd/MM/yyyy")
        {
            if (row.ContainsKey(column))
            {
                return DateTime.SpecifyKind(DateTime.ParseExact(row[column], format, CultureInfo.InvariantCulture), DateTimeKind.Utc);
            }

            return null;
        }

        public static decimal GetDecimal(this TableRow row, string column)
        {
            return decimal.Parse(row[column]);
        }

        public static int GetInt(this TableRow row, string column)
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