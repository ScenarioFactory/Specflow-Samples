namespace Samples.AutomationFramework
{
    using System.Linq;

    public static class StringExtensions
    {
        public static string[] FromCsv(this string s)
        {
            return s.Split(",")
                .Select(e => e.Trim())
                .ToArray();
        }

        public static string ToCsv(this string[] values)
        {
            return string.Join(", ", values);
        }

        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsWhiteSpace(this string s)
        {
            char[] chars = s.ToCharArray();

            return chars.Length > 0 && chars.All(char.IsWhiteSpace);
        }

        public static bool IsEmptyOrWhiteSpace(this string s)
        {
            return s.IsEmpty() || s.IsWhiteSpace();
        }
    }
}

