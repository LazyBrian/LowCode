using LowCode.Models;
using Microsoft.EntityFrameworkCore;

public class LowCodeDbContext : DbContext
{
    public LowCodeDbContext(DbContextOptions<LowCodeDbContext> options)
        : base(options)
    {

    }
    public DbSet<InternalEntity> Entities { get; set; }

    public DbSet<InternalAttribute> Attributes { get; set; }

    public DbSet<InternalAttributeType> AttributeTypes { get; set; }

    public DbSet<InternalUIForm> UIForms { get; set; }

    public DbSet<InternalUIView> UIViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InternalEntity>(entity =>
        {
            entity.HasKey(key => key.EntityId)
                .HasName("PK_Entity")
                .IsClustered(false);

            entity.ToTable("InternalEntity");

            entity.HasIndex(e => e.LogicalName, "idx_entity_logicalname");
            entity.HasIndex(e => e.DisplayName, "idx_entity_displayname");

            entity.Property(e => e.LogicalName).HasMaxLength(64);
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.IsCustomEntity)
                .IsRequired()
                .HasDefaultValueSql("((0))");
            entity.Property(e=>e.EntityMask).HasDefaultValueSql("((0))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .ValueGeneratedOnAddOrUpdate();

        });

        modelBuilder.Entity<InternalAttribute>(entity=>{
            entity.HasKey(e=>e.AttributeId)
            .HasName("PK_Attribute")
            .IsClustered(false);

            entity.ToTable("InternalAttribute");

            entity.HasIndex(e=>e.LogicalName,"idx_attribute_logicalname");
            entity.HasIndex(e=>e.DisplayName,"idx_attribute_displayname");

            entity.Property(e=>e.LogicalName).HasMaxLength(64);
            entity.Property(e=>e.DisplayName).HasMaxLength(100);
            entity.Property(e=>e.AttributeMask).HasDefaultValueSql("((0))");
            entity.Property(e=>e.DefaultValue).HasMaxLength(64);
            entity.Property(e=>e.IsCustomField).HasDefaultValueSql("((0))");
            entity.Property(e=>e.IsPKAttribute).HasDefaultValueSql("((0))");
            entity.Property(e=>e.MinValue).HasColumnType("decimal");
            entity.Property(e=>e.MaxValue).HasColumnType("decimal");
            entity.Property(e=>e.IsActive)
            .IsRequired()
            .HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<InternalAttributeType>(entity=>{
            entity.HasKey(e=>e.AttributeTypeId)
            .HasName("PK_AttributeType")
            .IsClustered(false);

            entity.ToTable("InternalAttributeType");
            
            entity.HasIndex(e=>e.Name,"idx_name");

            entity.Property(e=>e.Name).HasMaxLength(64);

            entity.Property(e=>e.CustomType).HasMaxLength(64);
        });

        modelBuilder.Entity<InternalUIForm>(entity=>{
            entity.HasKey(e=>e.FormId)
            .HasName("PK_UIForm")
            .IsClustered(false);

            entity.ToTable("InternalUIForm");

            entity.HasIndex(e=>e.FormName,"idx_uiform_formname");

            entity.Property(e=>e.FormName).HasMaxLength(100);
            entity.Property(e=>e.Description).HasMaxLength(500);
            entity.Property(e=>e.IsDefault)
            .IsRequired()
            .HasDefaultValueSql("((0))");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .ValueGeneratedOnAddOrUpdate();
        });

        modelBuilder.Entity<InternalUIView>(entity=>{
            entity.HasKey(e=>e.ViewId)
            .HasName("PK_UIView")
            .IsClustered(false);

            entity.ToTable("InternalUiView");

            entity.HasIndex(e=>e.ViewName,"idx_uiview_viewname");

            entity.Property(e=>e.ViewName).HasMaxLength(64);
            entity.Property(e=>e.IsDefault)
                .IsRequired()
                .HasDefaultValue(false);       

        });



    }

}