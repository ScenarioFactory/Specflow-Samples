namespace AutoWorkshop.Specs.Screenplay.Screenplay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Actor
    {
        private readonly IDictionary<Type, IAbility> _abilities = new Dictionary<Type, IAbility>();

        public void AttemptsTo(params ITask[] tasks)
        {
            tasks.ToList().ForEach(t => t.PerformAs(this));
        }

        public void Can(IAbility ability)
        {
            _abilities.Add(ability.GetType(), ability);
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
    }
}
