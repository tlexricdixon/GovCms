using CmsMvc.Models;
using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsMvc.Controllers;

public sealed class PageController(LocalDbContext db) : Controller
{
    [HttpGet("pages/{slug}")]
    public async Task<IActionResult> Details(
        string slug,
        CancellationToken cancellationToken)
    {
        var page = await db.Pages
            .AsNoTracking()
            .Include(page => page.PageBlocks)
            .SingleOrDefaultAsync(
                page => page.Slug == slug && page.IsPublished && page.IsActive,
                cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        var model = new PageDisplayViewModel
        {
            Title = page.Title,
            Slug = page.Slug,
            Blocks = page.PageBlocks
                .OrderBy(block => block.SortOrder)
                .ThenBy(block => block.Id)
                .Select(block => new PageBlockDisplayViewModel
                {
                    BlockType = block.BlockType,
                    HeadingText = block.HeadingText,
                    HeadingLevel = block.HeadingLevel,
                    ParagraphText = block.ParagraphText,
                    LinkText = block.LinkText,
                    LinkUrl = IsAllowedLink(block.LinkUrl) ? block.LinkUrl : null,
                    OpenInNewWindow = block.OpenInNewWindow
                })
                .ToList()
        };

        return View(model);
    }

    private static bool IsAllowedLink(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (value.StartsWith('/') && !value.StartsWith("//"))
        {
            return true;
        }

        return Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
            uri.Scheme is "http" or "https" or "mailto";
    }
}
