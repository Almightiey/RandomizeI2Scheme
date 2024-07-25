using System;
using System.Collections.Generic;

namespace I2Scheme.Persistece.Models;

public partial class LinkStyle
{
    public int Id { get; set; }

    public int? LineWidth { get; set; }

    public int? LinkColor { get; set; }

    public int? ArrowStyle { get; set; }

    public string? ArrowStyleInString { get; set; }

    public int? RelationshipId { get; set; }

    public virtual RelationshipInfo? Relationship { get; set; }
}
