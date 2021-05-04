namespace AutoWorkshop.Specs.Stateless.Extensions
{
    using System;
    using System.Collections.Generic;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static void ForEach(this IEnumerable<TableRow> rows, Action<TableRow> action)
        {
            foreach (var row in rows)
            {
                action(row);
            }
        }
    }
}