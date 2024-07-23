using AutoMapper;
using JE.OMDb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models;

namespace JE.OMDb.WebAPI.Controllers;

public class MoviesController : ControllerBase
{
    private readonly OmdbService _omdbService;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private const string LatestSearchesKey = "LatestSearches";

    public MoviesController(OmdbService omdbService, IMemoryCache memoryCache, IMapper mapper)
    {
        _omdbService = omdbService;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    [HttpGet("search")]
    public async Task<SearchResponseDTO> SearchMoviesByTitle(string title)
    {
        var latestSearches = _memoryCache.Get<List<string>>(LatestSearchesKey) ?? new List<string>();

        if (!latestSearches.Contains(title))
        {
            latestSearches.Add(title);
        }

        if (latestSearches.Count > 5)
        {
            latestSearches = latestSearches.Skip(latestSearches.Count - 5).ToList();
        }
        _memoryCache.Set(LatestSearchesKey, latestSearches);

        var result = await _omdbService.SearchMoviesByTitleAsync(title);
        return _mapper.Map<SearchResponseDTO>(result);   
    }

    [HttpGet("{imdbID}")]
    public async Task<MovieDTO> GetMovieDetails(string imdbID)
    {
        var movie = await _omdbService.GetMovieDetailsByImdbIDAsync(imdbID);
        return _mapper.Map<MovieDTO>(movie);
    }

    [HttpGet("latest-searches")]
    public List<string> GetLatestSearches()
    {
        return _memoryCache.Get<List<string>>(LatestSearchesKey) ?? new List<string>();
    }
}
