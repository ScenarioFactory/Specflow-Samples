namespace AutoWorkshop.Specs.Screenplay.Database.Tasks
{
    using Abilities;
    using Pattern;

    public abstract class DatabaseTask : ITask
    {
        public abstract void PerformAs(IActor actor, string connectionString);

        public void PerformAs(IActor actor)
        {
            PerformAs(actor, actor.Using<UseMySqlDatabase>().ConnectionString);
        }
    }
}