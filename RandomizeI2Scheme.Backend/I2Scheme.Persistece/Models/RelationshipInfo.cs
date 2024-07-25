using System;
using System.Collections.Generic;

namespace I2Scheme.Persistece.Models;

public partial class RelationshipInfo
{
    public int Id { get; set; }

    public string SourceIconId { get; set; }

    public string DestIconId { get; set; }

    public int? SchemeId { get; set; }

    public string? Label { get; set; }

    public string? Identifier { get; set; }

    public virtual ICollection<AtributeInfo> AtributeInfos { get; set; } = new List<AtributeInfo>();

    public virtual LinkStyle? LinkStyle { get; set; }

    public virtual I2scheme? Scheme { get; set; }
}
