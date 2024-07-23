using AutoMapper;
using JE.OMDb.DomainModels;

namespace Models.Mapping;

public class SearchResponseDTOMappingProfile : Profile
{
    public SearchResponseDTOMappingProfile()
    {
        CreateMap<SearchResponse, SearchResponseDTO>();
    }
}
