namespace AutoWorkshop.Specs.Screenplay.Extensions
{
    using Newtonsoft.Json;

    public static class JsonExtensions
    {
        public static string ToJson<T>(this T o) => JsonConvert.SerializeObject(o, Formatting.Indented);
    }
}
