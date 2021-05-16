namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using RestSharp;
    using Screenplay;

    public abstract class RestCall : IQuestion<IRestResponse>
    {
        protected abstract IRestResponse AskAs(IActor actor, IRestClient restClient);

        public IRestResponse AskAs(IActor actor)
        {
            return AskAs(actor, actor.Using<CallRestApi>().RestClient);
        }
    }
}