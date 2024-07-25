using System;
using System.Collections.Generic;

namespace I2Scheme.Persistece.Models;

public partial class IconFrame
{
    public int Id { get; set; }

    public int? Margin { get; set; }

    public int? Color { get; set; }

    public int? IconInfoId { get; set; }

    public bool? IsActive { get; set; }

    public virtual IconInfo? IconInfo { get; set; }
}
