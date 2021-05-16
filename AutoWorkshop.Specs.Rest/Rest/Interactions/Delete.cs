namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using RestSharp;
    using Screenplay;

    public class Delete : RestCall
    {
        private readonly string _uri;

        private Delete(string uri)
        {
            _uri = uri;
        }

        public static Delete ResourceAt(string uri)
        {
            return new Delete(uri);
        }

        protected override IRestResponse AskAs(IActor actor, IRestClient restClient)
        {
            return restClient.Delete(new RestRequest(_uri));
        }
    }
}
