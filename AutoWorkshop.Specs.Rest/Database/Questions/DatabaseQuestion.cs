namespace AutoWorkshop.Specs.Rest.Database.Questions
{
    using Database;
    using Screenplay;

    public abstract class DatabaseQuestion<TAnswer> : IQuestion<TAnswer>
    {
        protected abstract TAnswer AskAs(IActor actor, string connectionString);

        public TAnswer AskAs(IActor actor)
        {
            return AskAs(actor, actor.Using<UseMySqlDatabase>().ConnectionString);
        }
    }
}