using AutoMapper;
using I2Scheme.Application.Common.Mapping;
using I2Scheme.Persistece.Models;

namespace RandomizeI2Scheme.Api.Models.Dto;

public class I2SchemeDto : IMapWith<I2scheme>
{
    public int id { get; set; }
    public string Name { get; set; } = "";
    public List<IconInfoDto> Icons { get; set; }
    public List<RelationshipInfoDto>? Relationships { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<I2SchemeDto, I2scheme>()
            .ForMember(obj => obj.IconInfos,
                opt => opt.MapFrom(scheme => scheme.Icons))
            .ForMember(obj => obj.RelationshipInfos,
                opt => opt.MapFrom(scheme => scheme.Relationships))
            .ForMember(obj => obj.SchemeName,
                opt => opt.MapFrom(scheme => scheme.Name)).
                ForMember(obj => obj.Id,
                opt => opt.MapFrom(scheme => scheme.id));

        profile.CreateMap<I2scheme, I2SchemeDto>()
            .ForMember(obj => obj.Icons,
                opt => opt.MapFrom(scheme => scheme.IconInfos))
            .ForMember(obj => obj.Relationships,
                opt => opt.MapFrom(scheme => scheme.RelationshipInfos))
            .ForMember(obj => obj.Name,
                opt => opt.MapFrom(scheme => scheme.SchemeName))
            .ForMember(obj => obj.id,
                opt => opt.MapFrom(scheme => scheme.Id));
    }
}