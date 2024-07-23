using JE.OMDb.Common;
using JE.OMDb.DomainModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JE.OMDb.Services.Interfaces;

namespace JE.OMDb.Services;

public class OmdbService : IOmdbService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _settings;

    public OmdbService(HttpClient httpClient, IOptions<AppSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<SearchResponse> SearchMoviesByTitleAsync(string title)
    {
        try
        {
            var url = $"{_settings.BaseUrl}/?s={title}&apikey={_settings.ApiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SearchResponse>(content)!;
        } 
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while searching for movies: {ex.StackTrace}");
        }
    }

    public async Task<Movie> GetMovieDetailsByImdbIDAsync(string imdbID)
    {
        try
        {
            var url = $"{_settings.BaseUrl}/?i={imdbID}&apikey={_settings.ApiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Movie>(content)!;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while getting details for movies: {ex.StackTrace}");
        }
    }
}
