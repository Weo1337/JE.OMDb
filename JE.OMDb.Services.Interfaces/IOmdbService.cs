using JE.OMDb.DomainModels;

namespace JE.OMDb.Services.Interfaces;

public interface IOmdbService
{
    Task<SearchResponse> SearchMoviesByTitleAsync(string title);

    Task<Movie> GetMovieDetailsByImdbIDAsync(string imdbID);
}
