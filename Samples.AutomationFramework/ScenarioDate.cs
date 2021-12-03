namespace Samples.AutomationFramework
{
    using System;
    using System.Globalization;
    using TechTalk.SpecFlow;

    public class ScenarioDate
    {
        private readonly DateTime _value;

        public ScenarioDate(string dateString)
        {
            _value = DateTime.SpecifyKind(DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc).Date;
        }

        public static implicit operator DateTime(ScenarioDate t) => t._value;

        [Binding]
        private class ScenarioDateStepArgumentTransformation
        {
            [StepArgumentTransformation]
            private ScenarioDate ToScenarioDate(string value) => new ScenarioDate(value);
        }
    }
}
