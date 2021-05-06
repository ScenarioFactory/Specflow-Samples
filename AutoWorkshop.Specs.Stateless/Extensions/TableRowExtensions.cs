namespace AutoWorkshop.Specs.Stateless.Extensions
{
    using System;
    using System.Collections.Generic;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static void ForEach(this IEnumerable<TableRow> rows, Action<TableRow> action)
        {
            foreach (TableRow row in rows)
            {
                action(row);
            }
        }

        public static void ForEach(this TableRow row, Action<KeyValuePair<string, string>> action)
        {
            foreach (KeyValuePair<string, string> cell in row)
            {
                action(cell);
            }
        }

        public static int ParseInt(this TableRow row, string column)
        {
            return int.Parse(row[column]);
        }
    }
}