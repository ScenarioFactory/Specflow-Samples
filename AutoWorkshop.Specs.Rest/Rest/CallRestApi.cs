namespace AutoWorkshop.Specs.Rest.Rest
{
    using RestSharp;
    using Screenplay;

    public class CallRestApi : IAbility
    {
        public RestClient RestClient { get; }

        private CallRestApi(RestClient restClient)
        {
            RestClient = restClient;
        }

        public static CallRestApi Using(RestClient restClient)
        {
            return new CallRestApi(restClient);
        }
    }
}
