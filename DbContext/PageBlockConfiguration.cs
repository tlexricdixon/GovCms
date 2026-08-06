using Manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContexts;

public sealed class PageBlockConfiguration : IEntityTypeConfiguration<PageBlock>
{
    public void Configure(EntityTypeBuilder<PageBlock> builder)
    {
        builder.ToTable("PageBlocks");

        builder.Property(block => block.BlockType)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(block => block.HeadingText)
            .HasMaxLength(300);

        builder.Property(block => block.ParagraphText)
            .HasMaxLength(8000);

        builder.Property(block => block.LinkText)
            .HasMaxLength(300);

        builder.Property(block => block.LinkUrl)
            .HasMaxLength(2048);

        builder.HasIndex(block => new { block.PageId, block.SortOrder });
    }
}
