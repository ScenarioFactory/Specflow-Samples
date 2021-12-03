namespace Samples.AutomationFramework
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        private const string NullIdentifier = "?";

        public static void CompareToInstance<T>(this TableRow row, T instance, bool treatEmptyAndNullStringsAsEquivalent = false) where T : new()
        {
            T rowInstance = row.CreateInstance<T>();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            propertyInfos.ForEach(p =>
            {
                object expectedValue = p.GetValue(rowInstance);
                object actualValue = p.GetValue(instance);

                if (treatEmptyAndNullStringsAsEquivalent && p.PropertyType == typeof(string))
                {
                    expectedValue = expectedValue != null && expectedValue.ToString() != string.Empty ? expectedValue : null;
                    actualValue = actualValue != null && actualValue.ToString() != string.Empty ? actualValue : null;
                }

                actualValue.Should().Be(expectedValue, $"Value incorrect for column: {p.Name} in row: {row.ToHeaderAndValueString()}");
            });
        }

        public static T CreateInstance<T>(this TableRow row) where T : new()
        {
            T instance = new T();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            propertyInfos.ForEach(p =>
            {
                if (row.Select(cell => cell.Key.ToUpper()).Contains(p.Name.ToUpper()))
                {
                    string tableRowValue = row.GetTableRowValueForPropertyNameIgnoreCase(p.Name);
                    if (tableRowValue == NullIdentifier)
                    {
                        p.SetValue(instance, null);
                    }
                    else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?))
                    {
                        p.SetValue(instance, int.Parse(tableRowValue));
                    }
                    else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?))
                    {
                        p.SetValue(instance, decimal.Parse(tableRowValue));
                    }
                    else if (p.PropertyType.IsEnum)
                    {
                        p.SetValue(instance, Enum.Parse(p.PropertyType, tableRowValue.Replace(" ", string.Empty)));
                    }
                    else
                    {
                        p.SetValue(instance, tableRowValue);
                    }
                }
            });

            return instance;
        }

        private static string GetTableRowValueForPropertyNameIgnoreCase(this TableRow row, string propertyName)
        {
            List<string> keys = row.Select(cell => cell.Key.ToUpper()).ToList();
            int positionOfMatchingKey = keys.IndexOf(propertyName.ToUpper());

            if (positionOfMatchingKey != -1)
            {
                return row.Values.ToList()[positionOfMatchingKey];
            }

            return null;
        }

        public static bool ParseBoolean(this TableRow tableRow, string column)
        {
            return tableRow.ParseNullableBoolean(column).Value;
        }

        public static bool? ParseNullableBoolean(this TableRow tableRow, string column)
        {
            string[] additionalTrueLiterals = { "YES", "Yes", "yes", "1" };
            string[] additionalFalseLiterals = { "NO", "No", "no", "0" };

            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    if (additionalTrueLiterals.Contains(columnValue))
                    {
                        return true;
                    }

                    if (additionalFalseLiterals.Contains(columnValue))
                    {
                        return false;
                    }

                    return bool.Parse(columnValue);
                }
            }

            return null;
        }

        public static DateTime ParseDate(this TableRow tableRow, string column, string format = "d/MM/yyyy")
        {
            string columnValue = tableRow[column];
            return DateTime.SpecifyKind(DateTime.ParseExact(columnValue, format, CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static DateTime ParseDateAndTime(this TableRow tableRow, string column)
        {
            string columnValue = tableRow[column];
            return DateTime.SpecifyKind(DateTime.ParseExact(columnValue, "yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static DateTime? ParseNullableDate(this TableRow tableRow, string column, string format = "d/MM/yyyy")
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    return DateTime.SpecifyKind(DateTime.ParseExact(columnValue, format, CultureInfo.InvariantCulture), DateTimeKind.Utc);
                }
            }

            return null;
        }

        public static decimal ParseDecimal(this TableRow tableRow, string column)
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    return decimal.Parse(columnValue);
                }
            }

            return 0;
        }

        public static decimal? ParseNullableDecimal(this TableRow tableRow, string column)
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    return decimal.Parse(columnValue);
                }
            }

            return null;
        }

         public static T ParseEnum<T>(this TableRow tableRow, string column)
        {
            return (T)Enum.Parse(typeof(T), tableRow[column].Replace(" ", string.Empty));
        }

        public static object ParseNullableEnum<T>(this TableRow tableRow, string column)
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    return Enum.Parse(typeof(T), tableRow[column].Replace(" ", string.Empty));
                }
            }

            return null;
        }
        
        public static int ParseInt(this TableRow tableRow, string column)
        {
            return int.Parse(tableRow[column]);
        }

        public static int? ParseNullableInt(this TableRow tableRow, string column)
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (!string.IsNullOrEmpty(columnValue) && columnValue != NullIdentifier && columnValue != "<Blank>")
                {
                    return (int)double.Parse(columnValue);
                }
            }

            return null;
        }

        public static string ParseStringIfPresentElseEmpty(this TableRow tableRow, string column)
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    return columnValue;
                }
            }

            return string.Empty;
        }

        public static string ParseNullableString(this TableRow tableRow, string column)
        {
            if (tableRow.ContainsKey(column))
            {
                string columnValue = tableRow[column];

                if (columnValue != NullIdentifier)
                {
                    return columnValue;
                }
            }

            return null;
        }

        public static IReadOnlyCollection<TableRow> PollForUnmatchedRows<TComparisonItem>(
            this IEnumerable<TableRow> rows,
            Func<IEnumerable<TComparisonItem>> getComparisonData,
            Func<TComparisonItem, TableRow, bool> isMatchingRow,
            int maxNumChecks = 10,
            int waitIntervalSeconds = 2)
        {
            List<TableRow> unmatchedRows = new List<TableRow>();

            bool FindUnmatchedRows()
            {
                IEnumerable<TComparisonItem> comparisonItems = getComparisonData();

                unmatchedRows = rows
                    .Where(row => comparisonItems.None(c => isMatchingRow(c, row)))
                    .ToList();

                return unmatchedRows.None();
            }

            Poller.PollForSuccess(FindUnmatchedRows, maxNumChecks, waitIntervalSeconds);

            return unmatchedRows;
        }

        public static bool PollForMatch<TComparisonItem>(
            this TableRow row,
            Func<TComparisonItem> getComparisonData,
            Func<TComparisonItem, TableRow, bool> isMatchingRow,
            int maxNumChecks = 10,
            int waitIntervalSeconds = 2)
        {
            bool Match()
            {
                TComparisonItem comparisonItem = getComparisonData();

                return isMatchingRow(comparisonItem, row);
            }

            bool foundMatch = Poller.PollForSuccess(Match, maxNumChecks, waitIntervalSeconds);

            return foundMatch;
        }

        public static string ToHeaderAndValueString(this TableRow row)
        {
            return $"[{string.Join(", ", row.Select(cell => $"{cell.Key}: {cell.Value}"))}]";
        }

        public static string ToHeaderAndValueString(this IEnumerable<TableRow> rows)
        {
            return $"{string.Join(", ", rows.Select(r => r.ToHeaderAndValueString()))}";
        }
    }
}
