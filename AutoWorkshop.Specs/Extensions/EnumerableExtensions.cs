namespace AutoWorkshop.Specs.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> source) => !source.Any();
    }
}
