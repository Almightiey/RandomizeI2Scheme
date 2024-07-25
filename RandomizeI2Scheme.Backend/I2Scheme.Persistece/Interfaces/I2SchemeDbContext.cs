using I2Scheme.Persistece.Models;
using Microsoft.EntityFrameworkCore;

namespace I2Scheme.Persistece.DI.Interfaces;

public interface I2SchemeDbContext
{
    DbSet<AtributeInfo> AtributeInfos { get; set; }
    DbSet<I2scheme> I2schemes { get; set; }
    DbSet<IconFrame> IconFrames { get; set; }
    DbSet<IconInfo> IconInfos { get; set; }
    DbSet<LinkStyle> LinkStyles { get; set; }
    DbSet<RelationshipInfo> RelationshipInfos { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);
}

