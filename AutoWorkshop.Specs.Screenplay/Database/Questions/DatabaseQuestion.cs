namespace AutoWorkshop.Specs.Screenplay.Database.Questions
{
    using Pattern;

    public abstract class DatabaseQuestion<TAnswer> : IQuestion<TAnswer>
    {
        public abstract TAnswer AskAs(IActor actor, string connectionString);

        public TAnswer AskAs(IActor actor)
        {
            return AskAs(actor, actor.Using<UseMySqlDatabase>().ConnectionString);
        }
    }
}