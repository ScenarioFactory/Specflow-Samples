namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using RestSharp;
    using Screenplay;

    public class Get : RestCall
    {
        private readonly string _uri;

        private Get(string uri)
        {
            _uri = uri;
        }

        public static Get From(string uri)
        {
            return new Get(uri);
        }

        protected override IRestResponse AskAs(IActor actor, IRestClient restClient)
        {
            return restClient.Get(new RestRequest(_uri, DataFormat.Json));
        }
    }
}
