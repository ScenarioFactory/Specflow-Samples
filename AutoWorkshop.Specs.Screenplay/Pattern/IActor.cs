namespace AutoWorkshop.Specs.Screenplay.Pattern
{
    public interface IActor
    {
        TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question);

        void AttemptsTo(params ITask[] tasks);

        void Can(IAbility ability);

        TAbility Using<TAbility>() where TAbility : IAbility;
    }
}
