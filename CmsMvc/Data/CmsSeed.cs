using CmsModels;
using DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CmsMvc.Data;

public static class CmsSeed
{
    public static async Task InitializeAsync(
        LocalDbContext db,
        CancellationToken cancellationToken = default)
    {
        await db.Database.EnsureCreatedAsync(cancellationToken);

        if (await db.Pages.AnyAsync(cancellationToken))
        {
            return;
        }

        var page = new Page
        {
            Title = "ISP CMS Prototype",
            Slug = "home",
            IsPublished = true,
            PublishedAt = DateTime.UtcNow,
            PageBlocks =
            [
                new PageBlock
                {
                    SortOrder = 1,
                    BlockType = BlockType.Heading,
                    HeadingText = "Accessible by default",
                    HeadingLevel = 2
                },
                new PageBlock
                {
                    SortOrder = 2,
                    BlockType = BlockType.Paragraph,
                    ParagraphText = "This page was loaded from SQLite and rendered through normal Razor encoding."
                },
                new PageBlock
                {
                    SortOrder = 3,
                    BlockType = BlockType.Link,
                    LinkText = "Illinois State Police",
                    LinkUrl = "https://isp.illinois.gov",
                    OpenInNewWindow = true
                }
            ]
        };

        db.Pages.Add(page);
        await db.SaveChangesAsync(cancellationToken);
    }
}
