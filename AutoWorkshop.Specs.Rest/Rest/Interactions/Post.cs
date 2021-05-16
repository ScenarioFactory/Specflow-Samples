namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using Extensions;
    using RestSharp;
    using Screenplay;

    public class Post : RestCall
    {
        private readonly string _body;
        private string _uri;

        private Post(string body)
        {
            _body = body;
        }

        public static Post Body<T>(T body)
        {
            return new Post(body.ToJson());
        }

        public Post To(string uri)
        {
            _uri = uri;
            return this;
        }

        protected override IRestResponse AskAs(IActor actor, IRestClient restClient)
        {
            var request = new RestRequest(_uri, DataFormat.Json)
                .AddJsonBody(_body);

            return restClient.Post(request);
        }
    }
}
