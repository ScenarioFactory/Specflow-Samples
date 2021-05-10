namespace AutoWorkshop.Specs.Screenplay.Extensions
{
    using System;
    using TechTalk.SpecFlow;

    public static class TableRowExtensions
    {
        public static void ForEach(this TableRows rows, Action<TableRow> action)
        {
            foreach (var row in rows)
            {
                action(row);
            }
        }
    }
}