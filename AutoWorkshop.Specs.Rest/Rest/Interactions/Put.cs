namespace AutoWorkshop.Specs.Rest.Rest.Interactions
{
    using Extensions;
    using RestSharp;
    using Screenplay;

    public class Put : RestCall
    {
        private readonly string _body;
        private string _uri;

        private Put(string body)
        {
            _body = body;
        }

        public static Put Body<T>(T body)
        {
            return new Put(body.ToJson());
        }

        public Put To(string uri)
        {
            _uri = uri;
            return this;
        }

        protected override IRestResponse AskAs(IActor actor, IRestClient restClient)
        {
            var request = new RestRequest(_uri, DataFormat.Json)
                .AddJsonBody(_body);

            return restClient.Put(request);
        }
    }
}
