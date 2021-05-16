namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using Extensions;
    using RestSharp;
    using Screenplay;

    public class Post : RestCall
    {
        private readonly string _resource;
        private string _uri;

        private Post(string resource)
        {
            _resource = resource;
        }

        public static Post Resource<T>(T resource)
        {
            return new Post(resource.ToJson());
        }

        public Post To(string uri)
        {
            _uri = uri;
            return this;
        }

        protected override IRestResponse AskAs(IActor actor, IRestClient restClient)
        {
            var request = new RestRequest(_uri, DataFormat.Json)
                .AddJsonBody(_resource);

            return restClient.Post(request);
        }
    }
}
