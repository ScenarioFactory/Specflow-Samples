namespace AutoWorkshop.Specs.Rest.Steps
{
    using System.Linq;
    using System.Net;
    using Dto;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class RestResponseSteps
    {
        private readonly RestResponseInfo _lastResponse;

        public RestResponseSteps(RestResponseInfo lastResponse)
        {
            _lastResponse = lastResponse;
        }

        [Then(@"I should receive an HTTP (.*) (?:Conflict|Created|No Content|Not Found|OK) response")]
        public void ThenIShouldReceiveAnHttpResponse(int statusCode)
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.StatusCode.Should().Be((HttpStatusCode)statusCode);
        }

        [Then(@"the content should contain '(.*)'")]
        public void ThenTheContentShouldContain(string expectedContent)
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.Content.Should().Contain(expectedContent);
        }

        [Then(@"I should receive the location of the created resource")]
        public void ThenIShouldReceiveTheLocationOfTheCreatedResource()
        {
            _lastResponse.Response.Should().NotBeNull();

            bool isLocationHeaderPresent = _lastResponse.Response.Headers.Any(p => p.Name == "Location");
            isLocationHeaderPresent.Should().BeTrue();
        }

        [Then(@"the response should be in JSON format")]
        public void ThenTheResponseShouldBeInJsonFormat()
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.ContentType.Should().Contain("application/json");
        }
    }
}
