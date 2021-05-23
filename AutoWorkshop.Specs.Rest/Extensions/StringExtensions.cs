namespace AutoWorkshop.Specs.Rest.Extensions
{
    public static class StringExtensions
    {
        public static bool Empty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
