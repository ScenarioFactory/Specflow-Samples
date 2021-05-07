namespace AutoWorkshop.Specs.Extensions
{
    using System;
    using System.Globalization;

    public static class MySqlDateExtensions
    {
        public static DateTime? FromMySqlDate(this string s)
        {
            const string emptyMysqlDate = "0000-00-00";
                
            if (s == emptyMysqlDate)
            {
                return null;
            }

            return DateTime.SpecifyKind(DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }
    }
}
