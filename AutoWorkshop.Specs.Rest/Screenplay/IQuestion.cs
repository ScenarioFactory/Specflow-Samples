namespace AutoWorkshop.Specs.Rest.Screenplay
{
    public interface IQuestion<out TAnswer>
    {
        TAnswer AskAs(IActor actor);
    }
}
