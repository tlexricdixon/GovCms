using CmsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContexts;

public sealed class PageBlockConfiguration
: IEntityTypeConfiguration<PageBlock>
{
    public void Configure(EntityTypeBuilder<PageBlock> builder)
    {
        builder.HasKey(block => block.Id);

        builder.Property(block => block.SortOrder)
            .IsRequired();

        builder.Property(block => block.BlockType)
            .IsRequired();

        builder.Property(block => block.HeadingText)
            .HasMaxLength(300);

        builder.Property(block => block.ParagraphText)
            .HasMaxLength(8000);

        builder.Property(block => block.LinkText)
            .HasMaxLength(300);

        builder.Property(block => block.LinkUrl)
            .HasMaxLength(2048);

        builder.HasOne(block => block.Page)
            .WithMany(page => page.PageBlocks)
            .HasForeignKey(block => block.PageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(block => new
        {
            block.PageId,
            block.SortOrder
        });
    }
}
