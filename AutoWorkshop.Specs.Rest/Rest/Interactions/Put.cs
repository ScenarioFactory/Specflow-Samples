namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using Extensions;
    using RestSharp;
    using Screenplay;

    public class Put : RestCall
    {
        private readonly string _resource;
        private string _uri;

        private Put(string resource)
        {
            _resource = resource;
        }

        public static Put Resource<T>(T resource)
        {
            return new Put(resource.ToJson());
        }

        public Put At(string uri)
        {
            _uri = uri;
            return this;
        }

        protected override IRestResponse AskAs(IActor actor, IRestClient restClient)
        {
            var request = new RestRequest(_uri, DataFormat.Json)
                .AddJsonBody(_resource);

            return restClient.Put(request);
        }
    }
}
