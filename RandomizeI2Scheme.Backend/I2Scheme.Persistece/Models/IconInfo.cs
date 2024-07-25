using System;
using System.Collections.Generic;

namespace I2Scheme.Persistece.Models;

public partial class IconInfo
{
    public int Id { get; set; }

    public int SchemeId { get; set; }

    public string Identifier { get; set; }

    public string? Label { get; set; }

    public string? Type { get; set; }

    public bool? Issamelable { get; set; }

    public virtual ICollection<AtributeInfo> AtributeInfos { get; set; } = new List<AtributeInfo>();

    public virtual IconFrame? IconFrame { get; set; }

    public virtual I2scheme? Scheme { get; set; }
}
