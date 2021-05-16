namespace AutoWorkshop.Specs.Rest.Extensions
{
    using Newtonsoft.Json;

    public static class JsonExtensions
    {
        public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

        public static string ToJson<T>(this T o) => JsonConvert.SerializeObject(o, Formatting.Indented);
    }
}
