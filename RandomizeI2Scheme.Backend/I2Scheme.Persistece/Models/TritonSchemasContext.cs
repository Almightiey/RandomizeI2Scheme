using I2Scheme.Persistece.DI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace I2Scheme.Persistece.Models;

public partial class TritonSchemasContext : DbContext, I2SchemeDbContext
{
    public TritonSchemasContext()
    {
        var isCreated = this.Database.EnsureCreated();
    }

    public TritonSchemasContext(DbContextOptions<TritonSchemasContext> options)
        : base(options)
    {
        var isCreated = this.Database.EnsureCreated();
    }

    public virtual DbSet<AtributeInfo> AtributeInfos { get; set; }

    public virtual DbSet<I2scheme> I2schemes { get; set; }

    public virtual DbSet<IconFrame> IconFrames { get; set; }

    public virtual DbSet<IconInfo> IconInfos { get; set; }

    public virtual DbSet<LinkStyle> LinkStyles { get; set; }

    public virtual DbSet<RelationshipInfo> RelationshipInfos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        => optionsBuilder.UseNpgsql("Host=10.10.68.66;Database=Triton_Schemas;Username=postgres;Password=Vertex25@");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtributeInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("atribute_info_pkey");

            entity.ToTable("atribute_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IconId).HasColumnName("icon_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.RelationshipId).HasColumnName("relationship_id");
            entity.Property(e => e.Value)
                .HasColumnType("character varying")
                .HasColumnName("value");

            entity.HasOne(d => d.Icon).WithMany(p => p.AtributeInfos)
                .HasForeignKey(d => d.IconId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("atribute_info_icon_id_fkey");

            entity.HasOne(d => d.Relationship).WithMany(p => p.AtributeInfos)
                .HasForeignKey(d => d.RelationshipId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("atribute_info_relationship_id_fkey");
        });

        modelBuilder.Entity<I2scheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("I2Scheme_pkey");

            entity.ToTable("I2Scheme");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SchemeName).HasColumnType("character varying");
        });

        modelBuilder.Entity<IconFrame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("icon_frame_pkey");

            entity.ToTable("icon_frame");

            entity.HasIndex(e => e.IconInfoId, "icon_frame_icon_info_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.IconInfoId).HasColumnName("icon_info_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Margin).HasColumnName("margin");

            entity.HasOne(d => d.IconInfo).WithOne(p => p.IconFrame)
                .HasForeignKey<IconFrame>(d => d.IconInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("icon_frame_icon_info_id_fkey");
        });

        modelBuilder.Entity<IconInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("icon_info_pkey");

            entity.ToTable("icon_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnType("character varying");
            entity.Property(e => e.Issamelable).HasColumnName("issamelable");
            entity.Property(e => e.Label).HasColumnType("character varying");
            entity.Property(e => e.SchemeId).HasColumnName("scheme_id");
            entity.Property(e => e.Type).HasColumnType("character varying");

            entity.HasOne(d => d.Scheme).WithMany(p => p.IconInfos)
                .HasForeignKey(d => d.SchemeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("icon_info_scheme_id_fkey");
        });

        modelBuilder.Entity<LinkStyle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("link_style_pkey");

            entity.ToTable("link_style");

            entity.HasIndex(e => e.RelationshipId, "link_style_relationship_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArrowStyle).HasColumnName("Arrow_Style");
            entity.Property(e => e.ArrowStyleInString)
                .HasColumnType("character varying")
                .HasColumnName("Arrow_Style_In_String");
            entity.Property(e => e.LineWidth).HasColumnName("Line_Width");
            entity.Property(e => e.LinkColor).HasColumnName("Link_Color");
            entity.Property(e => e.RelationshipId).HasColumnName("relationship_id");

            entity.HasOne(d => d.Relationship).WithOne(p => p.LinkStyle)
                .HasForeignKey<LinkStyle>(d => d.RelationshipId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("link_style_relationship_id_fkey");
        });

        modelBuilder.Entity<RelationshipInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("relationship_info_pkey");

            entity.ToTable("relationship_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DestIconId).HasColumnType("character varying");
            entity.Property(e => e.Identifier).HasColumnType("character varying");
            entity.Property(e => e.Label).HasColumnType("character varying");
            entity.Property(e => e.SchemeId).HasColumnName("scheme_id");
            entity.Property(e => e.SourceIconId).HasColumnType("character varying");

            entity.HasOne(d => d.Scheme).WithMany(p => p.RelationshipInfos)
                .HasForeignKey(d => d.SchemeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("relationship_info_scheme_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
