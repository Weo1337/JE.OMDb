using JE.OMDb.Common;
using JE.OMDb.DomainModels;
using JE.OMDb.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace OMDb.Tests.ServiceTests;

public class OmdbServiceTests
{
    private static Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private static HttpClient _httpClient;
    private static IOptions<AppSettings> _options;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _options = Options.Create(new AppSettings
        {
            BaseUrl = "http://www.omdbapi.com",
            ApiKey = "test_api_key"
        });
    }

    [Test]
    public async Task SearchMoviesByTitleAsync_ReturnsSearchResponse()
    {
        // Arrange
        var titleName = "Inception";
        var searchResponse = new SearchResponse { Search = new List<SearchResult> { new SearchResult { Title = titleName } } };
        var searchResponseJson = JsonConvert.SerializeObject(searchResponse);

        // Act
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(searchResponseJson)
            });

        var omdbService = new OmdbService(_httpClient, _options);
        var result = await omdbService.SearchMoviesByTitleAsync(titleName);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.Search?.Count(), Is.EqualTo(1));
        Assert.That(titleName, Is.EqualTo(result!.Search![0].Title));
    }

    [Test]
    public async Task GetMovieDetailsAsync_ReturnsMovie()
    {
        // Arrange
        var titleName = "Inception";
        var movie = new Movie { Title = titleName };
        var movieJson = JsonConvert.SerializeObject(movie);

        // Act
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(movieJson)
            });

        var omdbService = new OmdbService(_httpClient, _options);
        var result = await omdbService.GetMovieDetailsByImdbIDAsync("tt1375666");

        // Assert
        Assert.NotNull(result);
        Assert.That(titleName, Is.EqualTo(result.Title!));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _httpClient.Dispose();
    }
}