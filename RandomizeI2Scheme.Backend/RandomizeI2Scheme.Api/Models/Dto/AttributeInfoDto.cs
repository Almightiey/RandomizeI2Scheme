using AutoMapper;
using I2Scheme.Application.Common.Mapping;
using I2Scheme.Persistece.Models;

namespace RandomizeI2Scheme.Api.Models.Dto;

public class AttributeInfoDto : IMapWith<AtributeInfo>
{
    public string name { get; set; } 
    public string? value { get; set; } 
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AttributeInfoDto, AtributeInfo>()
            .ForMember(obj => obj.Name,
                opt => opt.MapFrom(icon => icon.name))
            .ForMember(obj => obj.Value,
                opt => opt.MapFrom(icon => icon.value));

        profile.CreateMap<AtributeInfo, AttributeInfoDto>()
            .ForMember(obj => obj.name,
                opt => opt.MapFrom(icon => icon.Name))
            .ForMember(obj => obj.value,
                opt => opt.MapFrom(icon => icon.Value));
    }
}