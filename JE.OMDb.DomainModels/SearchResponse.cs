namespace JE.OMDb.DomainModels;

public class SearchResponse
{
    public List<SearchResult>? Search { get; set; }
    public string? TotalResults { get; set; }
    public string? Response { get; set; }
}
