namespace AutoWorkshop.Specs.Rest.Extensions
{
    using System;

    public static class MySqlDateExtensions
    {
        private const string EmptyMysqlDate = "0000-00-00";

        public static string ToMySqlDate(this DateTime? dt)
        {
            return !dt.HasValue ? EmptyMysqlDate : dt.Value.ToString("yyyy-MM-dd");
        }
    }
}
