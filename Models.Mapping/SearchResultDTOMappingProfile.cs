using AutoMapper;
using JE.OMDb.DomainModels;

namespace Models.Mapping;

public class SearchResultDTOMappingProfile : Profile
{
    public SearchResultDTOMappingProfile()
    {
        CreateMap<SearchResult, SearchResultDTO>();
    }
}
