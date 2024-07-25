using System;
using System.Collections.Generic;

namespace I2Scheme.Persistece.Models;

public partial class AtributeInfo
{
    public int Id { get; set; }

    public string? Value { get; set; }

    public string? Name { get; set; }

    public int? IconId { get; set; }

    public int? RelationshipId { get; set; }

    public virtual IconInfo? Icon { get; set; }

    public virtual RelationshipInfo? Relationship { get; set; }
}
