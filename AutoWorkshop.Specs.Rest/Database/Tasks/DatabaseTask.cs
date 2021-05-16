namespace AutoWorkshop.Specs.Rest.Database.Tasks
{
    using Screenplay;

    public abstract class DatabaseTask : ITask
    {
        protected abstract void PerformAs(IActor actor, string connectionString);

        public void PerformAs(IActor actor)
        {
            PerformAs(actor, actor.Using<UseMySqlDatabase>().ConnectionString);
        }
    }
}