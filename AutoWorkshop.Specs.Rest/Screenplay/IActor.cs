namespace AutoWorkshop.Specs.Rest.Screenplay
{
    public interface IActor
    {
        TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question);

        void AttemptsTo(params ITask[] tasks);

        TResponse Calls<TResponse>(IQuestion<TResponse> request);

        TAbility Using<TAbility>() where TAbility : IAbility;

        IActor WhoCan(params IAbility[] abilities);
    }
}
