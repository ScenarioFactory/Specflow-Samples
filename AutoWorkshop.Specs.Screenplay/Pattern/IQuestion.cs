namespace AutoWorkshop.Specs.Screenplay.Pattern
{
    public interface IQuestion<TAnswer>
    {
        TAnswer AskAs(IActor actor);
    }
}
