namespace AutoWorkshop.Specs.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }

        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
    }
}
