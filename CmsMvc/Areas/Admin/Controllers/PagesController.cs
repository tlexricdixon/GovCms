using CmsMvc.Areas.Admin.Models;
using DbContexts;
using Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/pages")]
[AutoValidateAntiforgeryToken]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public sealed class PagesController(LocalDbContext db) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var pages = await db.Pages
            .AsNoTracking()
            .OrderBy(page => page.Title)
            .Select(page => new PageListItemViewModel
            {
                Id = page.Id,
                Title = page.Title,
                Slug = page.Slug,
                IsPublished = page.IsPublished,
                LastModified = page.LastModified
            })
            .ToListAsync(cancellationToken);

        return View(pages);
    }

    [HttpGet("create")]
    public IActionResult Create() => View(new PageCreateViewModel());

    [HttpPost("create")]
    public async Task<IActionResult> Create(
        PageCreateViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Title = model.Title.Trim();
        model.Slug = NormalizeSlug(model.Slug);

        if (await SlugExistsAsync(model.Slug, null, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "A page with this slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var page = new Page
        {
            Title = model.Title,
            Slug = model.Slug
        };

        db.Pages.Add(page);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Edit), new { id = page.Id });
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var model = await GetEditModelAsync(id, cancellationToken);
        return model is null ? NotFound() : View(model);
    }

    [HttpPost("edit/{id:int}")]
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
            if (!await PopulateEditStateAsync(model, cancellationToken))
            {
                return NotFound();
            }

            return View(model);
        }

        model.Title = model.Title.Trim();
        model.Slug = NormalizeSlug(model.Slug);

        if (await SlugExistsAsync(model.Slug, id, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.Slug), "A page with this slug already exists.");
        }

        if (!ModelState.IsValid)
        {
            if (!await PopulateEditStateAsync(model, cancellationToken))
            {
                return NotFound();
            }

            return View(model);
        }

        var page = await db.Pages.SingleOrDefaultAsync(page => page.Id == id, cancellationToken);
        if (page is null)
        {
            return NotFound();
        }

        page.Title = model.Title;
        page.Slug = model.Slug;
        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Page saved.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost("edit/{id:int}/blocks/add")]
    public async Task<IActionResult> AddBlock(
        int id,
        AddBlockViewModel model,
        CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(model.BlockType))
        {
            return BadRequest();
        }

        if (!await db.Pages.AnyAsync(page => page.Id == id, cancellationToken))
        {
            return NotFound();
        }

        var lastSortOrder = await db.PageBlocks
            .Where(block => block.PageId == id)
            .MaxAsync(block => (int?)block.SortOrder, cancellationToken) ?? 0;

        var block = CreateBlock(id, model.BlockType, lastSortOrder + 1);
        db.PageBlocks.Add(block);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(EditBlock), new { pageId = id, blockId = block.Id });
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
                block => block.Id == blockId && block.PageId == pageId,
                cancellationToken);

        return block is null ? NotFound() : View(ToEditModel(block));
    }

    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}")]
    public async Task<IActionResult> EditBlock(
        int pageId,
        int blockId,
        PageBlockEditViewModel model,
        CancellationToken cancellationToken)
    {
        if (blockId != model.Id || pageId != model.PageId)
        {
            return BadRequest();
        }

        var block = await db.PageBlocks.SingleOrDefaultAsync(
            block => block.Id == blockId && block.PageId == pageId,
            cancellationToken);

        if (block is null)
        {
            return NotFound();
        }

        if (block.BlockType != model.BlockType)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        ApplyBlockEdits(model, block);
        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Block saved.";
        return RedirectToAction(nameof(Edit), new { id = pageId });
    }

    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}/delete")]
    public async Task<IActionResult> DeleteBlock(
        int pageId,
        int blockId,
        CancellationToken cancellationToken)
    {
        var blocks = await GetOrderedBlocksAsync(pageId, cancellationToken);
        var block = blocks.SingleOrDefault(block => block.Id == blockId);
        if (block is null)
        {
            return NotFound();
        }

        db.PageBlocks.Remove(block);
        NormalizeBlockOrder(blocks.Where(candidate => candidate.Id != blockId));
        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Block deleted.";
        return RedirectToAction(nameof(Edit), new { id = pageId });
    }

    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}/move-up")]
    public Task<IActionResult> MoveBlockUp(
        int pageId,
        int blockId,
        CancellationToken cancellationToken) =>
        MoveBlockAsync(pageId, blockId, -1, cancellationToken);

    [HttpPost("edit/{pageId:int}/blocks/{blockId:int}/move-down")]
    public Task<IActionResult> MoveBlockDown(
        int pageId,
        int blockId,
        CancellationToken cancellationToken) =>
        MoveBlockAsync(pageId, blockId, 1, cancellationToken);

    [HttpPost("edit/{id:int}/publish")]
    public async Task<IActionResult> Publish(int id, CancellationToken cancellationToken)
    {
        var page = await db.Pages.SingleOrDefaultAsync(page => page.Id == id, cancellationToken);
        if (page is null)
        {
            return NotFound();
        }

        page.IsPublished = true;
        page.PublishedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Page published.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost("edit/{id:int}/unpublish")]
    public async Task<IActionResult> Unpublish(int id, CancellationToken cancellationToken)
    {
        var page = await db.Pages.SingleOrDefaultAsync(page => page.Id == id, cancellationToken);
        if (page is null)
        {
            return NotFound();
        }

        page.IsPublished = false;
        await db.SaveChangesAsync(cancellationToken);

        TempData["SuccessMessage"] = "Page unpublished.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    private async Task<IActionResult> MoveBlockAsync(
        int pageId,
        int blockId,
        int direction,
        CancellationToken cancellationToken)
    {
        var blocks = await GetOrderedBlocksAsync(pageId, cancellationToken);
        var currentIndex = blocks.FindIndex(block => block.Id == blockId);
        if (currentIndex < 0)
        {
            return NotFound();
        }

        var targetIndex = currentIndex + direction;
        if (targetIndex < 0 || targetIndex >= blocks.Count)
        {
            return RedirectToAction(nameof(Edit), new { id = pageId });
        }

        (blocks[currentIndex], blocks[targetIndex]) = (blocks[targetIndex], blocks[currentIndex]);
        NormalizeBlockOrder(blocks);
        await db.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Edit), new { id = pageId });
    }

    private async Task<PageEditViewModel?> GetEditModelAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var page = await db.Pages.AsNoTracking()
            .SingleOrDefaultAsync(page => page.Id == id, cancellationToken);

        if (page is null)
        {
            return null;
        }

        return new PageEditViewModel
        {
            Id = page.Id,
            Title = page.Title,
            Slug = page.Slug,
            IsPublished = page.IsPublished,
            PublishedAt = page.PublishedAt,
            LastModified = page.LastModified,
            PageBlocks = await GetBlockListAsync(id, cancellationToken)
        };
    }

    private async Task<IReadOnlyList<PageBlockListItemViewModel>> GetBlockListAsync(
        int pageId,
        CancellationToken cancellationToken)
    {
        var blocks = await db.PageBlocks.AsNoTracking()
            .Where(block => block.PageId == pageId)
            .OrderBy(block => block.SortOrder)
            .ThenBy(block => block.Id)
            .ToListAsync(cancellationToken);

        return blocks.Select(block => new PageBlockListItemViewModel
        {
            Id = block.Id,
            SortOrder = block.SortOrder,
            BlockType = block.BlockType,
            Summary = GetBlockSummary(block)
        }).ToList();
    }

    private async Task<bool> PopulateEditStateAsync(
        PageEditViewModel model,
        CancellationToken cancellationToken)
    {
        var state = await db.Pages.AsNoTracking()
            .Where(page => page.Id == model.Id)
            .Select(page => new
            {
                page.IsPublished,
                page.PublishedAt,
                page.LastModified
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (state is null)
        {
            return false;
        }

        model.IsPublished = state.IsPublished;
        model.PublishedAt = state.PublishedAt;
        model.LastModified = state.LastModified;
        model.PageBlocks = await GetBlockListAsync(model.Id, cancellationToken);
        return true;
    }

    private Task<List<PageBlock>> GetOrderedBlocksAsync(
        int pageId,
        CancellationToken cancellationToken) =>
        db.PageBlocks
            .Where(block => block.PageId == pageId)
            .OrderBy(block => block.SortOrder)
            .ThenBy(block => block.Id)
            .ToListAsync(cancellationToken);

    private Task<bool> SlugExistsAsync(
        string slug,
        int? exceptPageId,
        CancellationToken cancellationToken) =>
        db.Pages.AnyAsync(
            page => page.Slug == slug && (!exceptPageId.HasValue || page.Id != exceptPageId.Value),
            cancellationToken);

    private static string NormalizeSlug(string slug) => slug.Trim().ToLowerInvariant();

    private static PageBlock CreateBlock(int pageId, BlockType blockType, int sortOrder) =>
        blockType switch
        {
            BlockType.Heading => new PageBlock
            {
                PageId = pageId,
                SortOrder = sortOrder,
                BlockType = blockType,
                HeadingText = "New heading",
                HeadingLevel = 2
            },
            BlockType.Paragraph => new PageBlock
            {
                PageId = pageId,
                SortOrder = sortOrder,
                BlockType = blockType,
                ParagraphText = "New paragraph"
            },
            BlockType.Link => new PageBlock
            {
                PageId = pageId,
                SortOrder = sortOrder,
                BlockType = blockType,
                LinkText = "New link",
                LinkUrl = "/"
            },
            _ => throw new ArgumentOutOfRangeException(nameof(blockType))
        };

    private static PageBlockEditViewModel ToEditModel(PageBlock block) => new()
    {
        Id = block.Id,
        PageId = block.PageId,
        BlockType = block.BlockType,
        HeadingText = block.HeadingText,
        HeadingLevel = block.HeadingLevel,
        ParagraphText = block.ParagraphText,
        LinkText = block.LinkText,
        LinkUrl = block.LinkUrl,
        OpenInNewWindow = block.OpenInNewWindow
    };

    private static void ApplyBlockEdits(PageBlockEditViewModel source, PageBlock destination)
    {
        destination.HeadingText = null;
        destination.HeadingLevel = null;
        destination.ParagraphText = null;
        destination.LinkText = null;
        destination.LinkUrl = null;
        destination.OpenInNewWindow = false;

        switch (destination.BlockType)
        {
            case BlockType.Heading:
                destination.HeadingText = source.HeadingText?.Trim();
                destination.HeadingLevel = source.HeadingLevel;
                break;
            case BlockType.Paragraph:
                destination.ParagraphText = source.ParagraphText?.Trim();
                break;
            case BlockType.Link:
                destination.LinkText = source.LinkText?.Trim();
                destination.LinkUrl = source.LinkUrl?.Trim();
                destination.OpenInNewWindow = source.OpenInNewWindow;
                break;
            default:
                throw new InvalidOperationException("Unsupported block type.");
        }
    }

    private static string? GetBlockSummary(PageBlock block) => block.BlockType switch
    {
        BlockType.Heading => block.HeadingText,
        BlockType.Paragraph => block.ParagraphText,
        BlockType.Link => $"{block.LinkText} ({block.LinkUrl})",
        _ => null
    };

    private static void NormalizeBlockOrder(IEnumerable<PageBlock> blocks)
    {
        var index = 1;
        foreach (var block in blocks)
        {
            block.SortOrder = index++;
        }
    }
}
