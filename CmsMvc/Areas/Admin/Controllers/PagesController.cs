using CmsModels;
using CmsMvc.Areas.Admin.Models;
using DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsMvc.Areas.Admin.Controllers;


[Area("Admin")]
[Route("admin/pages")]
public sealed class PagesController(LocalDbContext db) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken)
    {
        var pages = await db.Pages
            .AsNoTracking()
            .OrderBy(page => page.Title)
            .ToListAsync(cancellationToken);

        return View(pages);
    }
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new PageCreateViewModel());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        PageCreateViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Slug = model.Slug.Trim().ToLowerInvariant();

        if (await db.Pages.AnyAsync(
                page => page.Slug == model.Slug,
                cancellationToken))
        {
            ModelState.AddModelError(
                nameof(model.Slug),
                "A page with this slug already exists.");
            return View(model);
        }

        var page = new Page
        {
            Title = model.Title.Trim(),
            Slug = model.Slug,
            IsPublished = false,
            PublishedAt = null
        };

        db.Pages.Add(page);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(
            nameof(Edit),
            new { id = page.Id });
    }
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(
    int id,
    CancellationToken cancellationToken)
    {
        var page = await db.Pages
            .AsNoTracking()
            .Include(page => page.PageBlocks
                .OrderBy(block => block.SortOrder))
            .SingleOrDefaultAsync(
                page => page.Id == id,
                cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        var model = new PageEditViewModel
        {
            Id = page.Id,
            Title = page.Title,
            Slug = page.Slug,
            IsPublished = page.IsPublished,
            PublishedAt = page.PublishedAt,
            LastModified = page.LastModified,
            PageBlocks = page.PageBlocks
                .OrderBy(block => block.SortOrder)
                .ToList()
        };

        return View(model);
    }
    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int id,
    PageEditViewModel model,
    CancellationToken cancellationToken)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            model.PageBlocks = await db.PageBlocks
                .AsNoTracking()
                .Where(block => block.PageId == id)
                .OrderBy(block => block.SortOrder)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        model.Title = model.Title.Trim();
        model.Slug = model.Slug.Trim().ToLowerInvariant();

        var slugExists = await db.Pages.AnyAsync(
            page =>
                page.Id != id &&
                page.Slug == model.Slug,
            cancellationToken);

        if (slugExists)
        {
            ModelState.AddModelError(
                nameof(model.Slug),
                "A page with this slug already exists.");

            model.PageBlocks = await db.PageBlocks
                .AsNoTracking()
                .Where(block => block.PageId == id)
                .OrderBy(block => block.SortOrder)
                .ToListAsync(cancellationToken);

            return View(model);
        }

        var page = await db.Pages
            .SingleOrDefaultAsync(
                page => page.Id == id,
                cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        page.Title = model.Title;
        page.Slug = model.Slug;

        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Page saved.";

        return RedirectToAction(
            nameof(Edit),
            new { id = page.Id });
    }
    [HttpPost("edit/{id:int}/blocks/add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddBlock(
    int id,
    AddBlockViewModel model,
    CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Edit), new { id });
        }

        var pageExists = await db.Pages.AnyAsync(
            page => page.Id == id,
            cancellationToken);

        if (!pageExists)
        {
            return NotFound();
        }

        var nextSortOrder = await db.PageBlocks
            .Where(block => block.PageId == id)
            .Select(block => (int?)block.SortOrder)
            .MaxAsync(cancellationToken) ?? 0;

        var block = new PageBlock
        {
            PageId = id,
            BlockType = model.BlockType,
            SortOrder = nextSortOrder + 1
        };

        switch (model.BlockType)
        {
            case BlockType.Heading:
                block.HeadingText = "New heading";
                block.HeadingLevel = 2;
                break;

            case BlockType.Paragraph:
                block.ParagraphText = "New paragraph";
                break;

            case BlockType.Link:
                block.LinkText = "New link";
                block.LinkUrl = "/";
                break;

            default:
                return BadRequest();
        }

        db.PageBlocks.Add(block);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(
            nameof(EditBlock),
            new
            {
                pageId = id,
                blockId = block.Id
            });
    }
    [HttpGet("edit/{pageId:int}/blocks/{blockId:int}")]
    public async Task<IActionResult> EditBlock(
    int pageId,
    int blockId,
    CancellationToken cancellationToken)
    {
        var block = await db.PageBlocks
            .AsNoTracking()
            .SingleOrDefaultAsync(
                block =>
                    block.Id == blockId &&
                    block.PageId == pageId,
                cancellationToken);

        if (block is null)
        {
            return NotFound();
        }

        return View(block);
    }
    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditBlock(
    int pageId,
    int blockId,
    PageBlock model,
    CancellationToken cancellationToken)
    {
        if (blockId != model.Id || pageId != model.PageId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var block = await db.PageBlocks
            .SingleOrDefaultAsync(
                block =>
                    block.Id == blockId &&
                    block.PageId == pageId,
                cancellationToken);

        if (block is null)
        {
            return NotFound();
        }

        switch (block.BlockType)
        {
            case BlockType.Heading:
                block.HeadingText = model.HeadingText?.Trim();
                block.HeadingLevel = model.HeadingLevel;
                break;

            case BlockType.Paragraph:
                block.ParagraphText = model.ParagraphText?.Trim();
                break;

            case BlockType.Link:
                block.LinkText = model.LinkText?.Trim();
                block.LinkUrl = model.LinkUrl?.Trim();
                block.OpenInNewWindow = model.OpenInNewWindow;
                break;

            default:
                return BadRequest();
        }

        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Block saved.";

        return RedirectToAction(
            nameof(Edit),
            new { id = pageId });
    }
    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBlock(
    int pageId,
    int blockId,
    CancellationToken cancellationToken)
    {
        var block = await db.PageBlocks
            .SingleOrDefaultAsync(
                block =>
                    block.Id == blockId &&
                    block.PageId == pageId,
                cancellationToken);

        if (block is null)
        {
            return NotFound();
        }

        db.PageBlocks.Remove(block);
        await db.SaveChangesAsync(cancellationToken);

        await NormalizeBlockOrder(pageId, cancellationToken);

        TempData["SuccessMessage"] = "Block deleted.";

        return RedirectToAction(
            nameof(Edit),
            new { id = pageId });
    }
    [HttpPost("edit/{id:int}/publish")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Publish(
    int id,
    CancellationToken cancellationToken)
    {
        var page = await db.Pages
            .SingleOrDefaultAsync(
                page => page.Id == id,
                cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        page.IsPublished = true;
        page.PublishedAt ??= DateTime.UtcNow;

        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Page published.";

        return RedirectToAction(nameof(Edit), new { id });
    }
    [HttpPost("edit/{id:int}/unpublish")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Unpublish(
    int id,
    CancellationToken cancellationToken)
    {
        var page = await db.Pages
            .SingleOrDefaultAsync(
                page => page.Id == id,
                cancellationToken);

        if (page is null)
        {
            return NotFound();
        }

        page.IsPublished = false;

        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Page unpublished.";

        return RedirectToAction(nameof(Edit), new { id });
    }
    private async Task NormalizeBlockOrder(
    int pageId,
    CancellationToken cancellationToken)
    {
        var blocks = await db.PageBlocks
            .Where(block => block.PageId == pageId)
            .OrderBy(block => block.SortOrder)
            .ThenBy(block => block.Id)
            .ToListAsync(cancellationToken);

        for (var index = 0; index < blocks.Count; index++)
        {
            blocks[index].SortOrder = index + 1;
        }

        await db.SaveChangesAsync(cancellationToken);
    }
    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}/move-up")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MoveBlockUp(
    int pageId,
    int blockId,
    CancellationToken cancellationToken)
    {
        var blocks = await db.PageBlocks
            .Where(block => block.PageId == pageId)
            .OrderBy(block => block.SortOrder)
            .ThenBy(block => block.Id)
            .ToListAsync(cancellationToken);

        var currentIndex = blocks.FindIndex(block => block.Id == blockId);

        if (currentIndex < 0)
        {
            return NotFound();
        }

        if (currentIndex == 0)
        {
            return RedirectToAction(nameof(Edit), new { id = pageId });
        }

        var current = blocks[currentIndex];
        var previous = blocks[currentIndex - 1];

        (current.SortOrder, previous.SortOrder) =
            (previous.SortOrder, current.SortOrder);

        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Edit), new { id = pageId });
    }
    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}/move-down")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MoveBlockDown(
    int pageId,
    int blockId,
    CancellationToken cancellationToken)
    {
        var blocks = await db.PageBlocks
            .Where(block => block.PageId == pageId)
            .OrderBy(block => block.SortOrder)
            .ThenBy(block => block.Id)
            .ToListAsync(cancellationToken);

        var currentIndex = blocks.FindIndex(block => block.Id == blockId);

        if (currentIndex < 0)
        {
            return NotFound();
        }

        if (currentIndex == blocks.Count - 1)
        {
            return RedirectToAction(nameof(Edit), new { id = pageId });
        }

        var current = blocks[currentIndex];
        var next = blocks[currentIndex + 1];

        (current.SortOrder, next.SortOrder) =
            (next.SortOrder, current.SortOrder);

        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Edit), new { id = pageId });
    }
}
