namespace Samples.AutomationFramework
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime ToLastThreeMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999, dt.Kind);
        }

        public static DateTime ToLastMinute(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0, dt.Kind);
        }

        public static DateTime ToStartOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, dt.Kind);
        }
    }
}