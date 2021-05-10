namespace AutoWorkshop.Specs.DomainModel.Extensions
{
    using System;
    using System.Globalization;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static bool ParseBoolean(this TableRow row, string column)
        {
            return bool.Parse(row[column]);
        }

        public static DateTime ParseDate(this TableRow row, string column)
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(row[column], "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static decimal ParseDecimal(this TableRow row, string column)
        {
            return decimal.Parse(row[column]);
        }
    }
}