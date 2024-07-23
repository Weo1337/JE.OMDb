using AutoMapper;
using JE.OMDb.DomainModels;

namespace Models.Mapping;

public class MovieDTOMappingProfile : Profile
{
    public MovieDTOMappingProfile()
    {
        CreateMap<Movie, MovieDTO>();
    }
}
