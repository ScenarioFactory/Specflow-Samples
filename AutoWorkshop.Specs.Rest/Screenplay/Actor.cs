namespace AutoWorkshop.Specs.Rest.Screenplay
{
    using System;
    using System.Collections.Generic;
    using Extensions;

    public class Actor : IActor
    {
        private readonly IDictionary<Type, IAbility> _abilities = new Dictionary<Type, IAbility>();

        public TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question)
        {
            return question.AskAs(this);
        }

        public void AttemptsTo(params ITask[] tasks)
        {
            tasks.ForEach(t => t.PerformAs(this));
        }

        public TResponse Calls<TResponse>(IQuestion<TResponse> request)
        {
            return AsksFor(request);
        }

        public TAbility Using<TAbility>() where TAbility : IAbility
        {
            Type t = typeof(TAbility);

            if (!_abilities.ContainsKey(t))
            {
                throw new ArgumentException(($"Actor does not have ability {t}"));
            }

            return (TAbility)_abilities[t];
        }

        public IActor WhoCan(params IAbility[] abilities)
        {
            abilities.ForEach(a => _abilities.Add(a.GetType(), a));
            return this;
        }
    }
}
