using System;
using System.Collections.Generic;

namespace I2Scheme.Persistece.Models;

public partial class I2scheme
{
    public int Id { get; set; }

    public string? SchemeName { get; set; }

    public virtual ICollection<IconInfo> IconInfos { get; set; } = new List<IconInfo>();

    public virtual ICollection<RelationshipInfo> RelationshipInfos { get; set; } = new List<RelationshipInfo>();
}
