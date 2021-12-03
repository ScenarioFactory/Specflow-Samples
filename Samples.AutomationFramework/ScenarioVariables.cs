namespace Samples.AutomationFramework
{
    using System.Linq;
    using TechTalk.SpecFlow;

    [Binding]
    public class ScenarioVariables
    {
        private readonly ScenarioContext _scenarioContext;

        public ScenarioVariables(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public string Get(string key)
        {
            return _scenarioContext.Get<string>(TrimSquareBrackets(key));
        }

        public void Set(string key, object value)
        {
            _scenarioContext[TrimSquareBrackets(key)] = value.ToString();
        }

        [StepArgumentTransformation]
        private TransformedTable ToTransformedTable(Table table)
        {
            table.Rows.ForEach(row =>
            {
                row.ForEach(cell =>
                {
                    row[cell.Key] = TransformVariable(cell.Value);
                });
            });

            return new TransformedTable(table);
        }

        [StepArgumentTransformation]
        private TransformedString ToTransformedString(string value)
        {
            return new TransformedString(TransformVariable(value));
        }

        [StepArgumentTransformation]
        private TransformedInt ToTransformedInt(string value)
        {
            return new TransformedInt(int.Parse(TransformVariable(value)));
        }

        private string TransformVariable(string value)
        {
            if (value.Contains("["))
            {
                _scenarioContext.ToList().ForEach(kvp =>
                {
                    string bracketedKey = $"[{kvp.Key}]";
                    value = value.Replace(bracketedKey, (string)kvp.Value);
                });
            }

            return value;
        }

        private static string TrimSquareBrackets(string value)
        {
            return value.Replace("[", string.Empty).Replace("]", string.Empty);
        }
    }

    public class TransformedTable
    {
        private readonly Table _value;
        
        public TransformedTable(Table transformedTable)
        {
            _value = transformedTable;
        }

        public static implicit operator Table(TransformedTable t) => t._value;

        public TableRows Rows => _value.Rows;
    }

    public class TransformedString
    {
        private readonly string _value;

        public TransformedString(string transformedString)
        {
            _value = transformedString;
        }

        public static implicit operator string(TransformedString t) => t._value;
    }

    public class TransformedInt
    {
        private readonly int _value;

        public TransformedInt(int transformedInt)
        {
            _value = transformedInt;
        }

        public static implicit operator int(TransformedInt t) => t._value;
    }
}
