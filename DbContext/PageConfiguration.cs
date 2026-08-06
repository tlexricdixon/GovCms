using Manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContexts;

public sealed class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("Pages");

        builder.Property(page => page.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(page => page.Slug)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(page => page.Slug)
            .IsUnique();

        builder.HasMany(page => page.PageBlocks)
            .WithOne(block => block.Page)
            .HasForeignKey(block => block.PageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
