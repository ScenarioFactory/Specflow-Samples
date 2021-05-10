namespace AutoWorkshop.Specs.Screenplay.Tasks
{
    using Abilities;
    using Drivers;
    using Pattern;

    public abstract class WebTask : ITask
    {
        public abstract void PerformAs(IActor actor, AutoWorkshopDriver driver);

        public void PerformAs(IActor actor)
        {
            PerformAs(actor, actor.Using<UseAutoWorkshop>().Driver);
        }
    }
}