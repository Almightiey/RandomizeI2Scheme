using AutoMapper;
using I2Scheme.Application.Common.Mapping;
using I2Scheme.Persistece.Models;

namespace RandomizeI2Scheme.Api.Models.Dto;

public class RelationshipInfoDto : IMapWith<RelationshipInfo>
{
    public string? Label { get; set; }
    public string Identifier { get; set; } 
    public List<AttributeInfoDto>? Attributes { get; set; }
    public string SourceIconId { get; set; }
    public string DestIconId { get; set; } 
    public LinkStyleDto? Style { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RelationshipInfoDto, RelationshipInfo>()
            .ForMember(obj => obj.AtributeInfos,
                opt => opt.MapFrom(icon => icon.Attributes))
            .ForMember(obj => obj.Label,
                opt => opt.MapFrom(icon => icon.Label))
            .ForMember(obj => obj.Identifier,
                opt => opt.MapFrom(icon => icon.Identifier))
            .ForMember(obj => obj.LinkStyle,
                opt => opt.MapFrom(icon => icon.Style))
            .ForMember(obj => obj.SourceIconId,
                opt => opt.MapFrom(icon => icon.SourceIconId))
            .ForMember(obj => obj.DestIconId,
                opt => opt.MapFrom(icon => icon.DestIconId));

        profile.CreateMap<RelationshipInfo, RelationshipInfoDto>()
            .ForMember(obj => obj.Attributes,
                opt => opt.MapFrom(icon => icon.AtributeInfos))
            .ForMember(obj => obj.Label,
                opt => opt.MapFrom(icon => icon.Label))
            .ForMember(obj => obj.Identifier,
                opt => opt.MapFrom(icon => icon.Identifier))
            .ForMember(obj => obj.Style,
                opt => opt.MapFrom(icon => icon.LinkStyle))
            .ForMember(obj => obj.SourceIconId,
                opt => opt.MapFrom(icon => icon.SourceIconId))
            .ForMember(obj => obj.DestIconId,
                opt => opt.MapFrom(icon => icon.DestIconId));
    }
}

public class LinkStyleDto : IMapWith<LinkStyle>
{
    public int? LineWidth { get; set; }
    public int? LinkColor { get; set; }

    public ArrowStyle? ArrowStyle { get; set; }
    public string? ArrowStyleInString { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<LinkStyleDto, LinkStyle>()
            .ForMember(obj => obj.ArrowStyle,
                opt => opt.MapFrom(icon => icon.ArrowStyle))
            .ForMember(obj => obj.ArrowStyleInString,
                opt => opt.MapFrom(icon => icon.ArrowStyleInString))
            .ForMember(obj => obj.LineWidth,
                opt => opt.MapFrom(icon => icon.LineWidth))
            .ForMember(obj => obj.LinkColor,
                opt => opt.MapFrom(icon => icon.LinkColor));

        profile.CreateMap<LinkStyle, LinkStyleDto>()
            .ForMember(obj => obj.ArrowStyle,
                opt => opt.MapFrom(icon => icon.ArrowStyle))
            .ForMember(obj => obj.ArrowStyleInString,
                opt => opt.MapFrom(icon => icon.ArrowStyleInString))
            .ForMember(obj => obj.LineWidth,
                opt => opt.MapFrom(icon => icon.LineWidth))
            .ForMember(obj => obj.LinkColor,
                opt => opt.MapFrom(icon => icon.LinkColor));
    }
}

public enum ArrowStyle
{
    ArrowNone = 0,
    ArrowOnHead,
    ArrowOnTail,
    ArrowOnBoth,
}
