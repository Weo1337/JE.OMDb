namespace Models;

public class SearchResponseDTO
{
    public List<SearchResultDTO>? Search { get; set; }
    public string? totalResults { get; set; }
    public string? Response { get; set; }
}
