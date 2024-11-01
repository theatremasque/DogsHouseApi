using AutoMapper;
using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;

namespace DogsHouse.API.Mapping;

public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<DogAddDto, Dog>();

        CreateProjection<Dog, DogDto>();
    }
}