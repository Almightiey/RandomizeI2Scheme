using AutoMapper;
using I2Scheme.Application.Common.Mapping;
using I2Scheme.Persistece.Models;

namespace RandomizeI2Scheme.Api.Models.Dto;

public class IconInfoDto : IMapWith<IconInfo>
{
    public string Identifier { get; set; }
    public string Label { get; set; }
    public string Type { get; set; }
    public bool? IsSameLable { get; set; }
    public List<AttributeInfoDto>? Attributes { get; set; }
    public IconFrameDto? Frame { get; set; } = new IconFrameDto();
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IconInfoDto, IconInfo>()
            .ForMember(obj => obj.AtributeInfos,
                opt => opt.MapFrom(icon => icon.Attributes))
            .ForMember(obj => obj.IconFrame,
                opt => opt.MapFrom(icon => icon.Frame))
            .ForMember(obj => obj.Label,
                opt => opt.MapFrom(icon => icon.Label))
            .ForMember(obj => obj.Identifier,
                opt => opt.MapFrom(icon => icon.Identifier))
            .ForMember(obj => obj.Type,
                opt => opt.MapFrom(icon => icon.Type))        
        .ForMember(obj => obj.Issamelable,
                opt => opt.MapFrom(icon => icon.IsSameLable));

        profile.CreateMap<IconInfo, IconInfoDto>()
            .ForMember(obj => obj.Attributes,
                opt => opt.MapFrom(icon => icon.AtributeInfos))
            .ForMember(obj => obj.Frame,
                opt => opt.MapFrom(icon => icon.IconFrame))
            .ForMember(obj => obj.Label,
                opt => opt.MapFrom(icon => icon.Label))
            .ForMember(obj => obj.Identifier,
                opt => opt.MapFrom(icon => icon.Identifier))
            .ForMember(obj => obj.IsSameLable,
                opt => opt.MapFrom(icon => icon.Issamelable))
            .ForMember(obj => obj.Type,
                opt => opt.MapFrom(icon => icon.Type));
    }
}

public class IconFrameDto : IMapWith<IconFrame>
{
    public int? Margin { get; set; }
    public int? Color { get; set; }
    public bool IsActive { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IconFrameDto, IconFrame>()
            .ForMember(obj => obj.Color,
                opt => opt.MapFrom(icon => icon.Color))
            .ForMember(obj => obj.Margin,
                opt => opt.MapFrom(icon => icon.Margin))
            .ForMember(obj => obj.IsActive,
                opt => opt.MapFrom(icon => icon.IsActive));

        profile.CreateMap<IconFrame, IconFrameDto>()
            .ForMember(obj => obj.Color,
                opt => opt.MapFrom(icon => icon.Color))
            .ForMember(obj => obj.Margin,
                opt => opt.MapFrom(icon => icon.Margin))
            .ForMember(obj => obj.IsActive,
                opt => opt.MapFrom(icon => icon.IsActive));
    }
}