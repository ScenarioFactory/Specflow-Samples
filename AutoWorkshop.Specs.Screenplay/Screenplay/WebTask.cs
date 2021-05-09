namespace AutoWorkshop.Specs.Screenplay.Screenplay
{
    using Abilities;
    using Drivers;

    public abstract class WebTask : ITask
    {
        public abstract void PerformAs(Actor actor, AutoWorkshopDriver driver);

        public void PerformAs(Actor actor)
        {
            PerformAs(actor, actor.Using<UseAutoWorkshop>().Driver);
        }
    }
}