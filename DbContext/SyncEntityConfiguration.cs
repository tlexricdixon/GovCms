namespace DbContexts;
using CmsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SyncEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : SyncEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.LastModified)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.NeedsSync)
            .IsRequired()
            .HasDefaultValue(true);

        ConfigureAdditional(builder);
    }

    // Override this in derived configs to apply custom relationships
    protected virtual void ConfigureAdditional(EntityTypeBuilder<T> builder) { }
}
public class PostConfiguration : SyncEntityConfiguration<Post>
{
    protected override void ConfigureAdditional(EntityTypeBuilder<Post> builder)
    {
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId);

        builder.HasMany(p => p.PostTags)
            .WithOne(pt => pt.Post)
            .HasForeignKey(pt => pt.PostId);
    }
}
public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.HasKey(pt => new { pt.PostId, pt.TagId });

        builder.HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

        builder.HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);
    }
}

