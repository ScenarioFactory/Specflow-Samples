namespace AutoWorkshop.Specs.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Framework;
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

        public static DateTime? GetDateOrDefault(this TableRow row, string column)
        {
            if (row.ContainsKey(column))
            {
                return row.GetDate(column);
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

        public static TableRow[] PollForUnmatchedRows<TKey, TComparisonItem>(
            this TableRows rows,
            Func<TKey, TComparisonItem> getComparisonData,
            Func<TableRow, TKey> selectKey,
            Func<TComparisonItem, TableRow, bool> isMatchingRow,
            int pollingLimit = 10,
            int pollingIntervalSeconds = 1)
        {
            var unmatchedRows = new List<TableRow>();

            bool FindUnmatchedRows()
            {
                unmatchedRows = rows
                    .Where(row =>
                    {
                        TKey key = selectKey(row);
                        TComparisonItem comparisonItem = getComparisonData(key);
                        return comparisonItem == null || !isMatchingRow(comparisonItem, row);
                    })
                    .ToList();

                return unmatchedRows.None();
            }

            Poller.PollForResult(FindUnmatchedRows, pollingLimit, pollingIntervalSeconds);

            return unmatchedRows.ToArray();
        }
    }
}