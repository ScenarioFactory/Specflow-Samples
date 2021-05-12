namespace AutoWorkshop.Specs.Screenplay.WebDriver.Questions
{
    using Pattern;

    public abstract class WebQuestion<TAnswer> : IQuestion<TAnswer>
    {
        public abstract TAnswer AskAs(IActor actor, AutoWorkshopDriver driver);

        public TAnswer AskAs(IActor actor)
        {
            return AskAs(actor, actor.Using<UseAutoWorkshop>().Driver);
        }
    }
}