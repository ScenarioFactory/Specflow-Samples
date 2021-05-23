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

        [Then(@"I should receive an HTTP 200 OK response")]
        public void ThenIShouldReceiveAnHttp200OkayResponse()
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"I should receive an HTTP 201 Created response")]
        public void ThenIShouldReceiveAnHttp201CreatedResponse()
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Then(@"I should receive an HTTP 204 No Content response")]
        public void ThenIShouldReceiveAnHttp204NoContentResponse()
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Then(@"I should receive an HTTP 404 Not Found response")]
        public void ThenIShouldReceiveAnHttpNotFoundResponse()
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Then(@"I should receive an HTTP 409 Conflict response")]
        public void ThenIShouldReceiveAnHttp409ConflictResponse()
        {
            _lastResponse.Response.Should().NotBeNull();

            _lastResponse.Response.StatusCode.Should().Be(HttpStatusCode.Conflict);
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
