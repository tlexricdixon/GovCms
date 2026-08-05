using DbContexts;
using Microsoft.AspNetCore.Authorization;
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
        //var page = await db.Pages
        //    .AsNoTracking()
        //    .Include(page => page.PageBlocks.OrderBy(block => block.SortOrder))
        //    .SingleOrDefaultAsync(
        //        page =>
        //            page.Slug == slug &&
        //            page.IsPublished &&
        //            page.IsActive,
        //        cancellationToken);

        //return page is null
        //    ? NotFound()
        //    : View(page);
        return RedirectToAction("Preview", new { slug });
    }
    [HttpGet("pages/preview/{slug}")]
    public async Task<IActionResult> Preview(
        string slug,
        CancellationToken cancellationToken)
    {
        //var page = await db.Pages
        //    .AsNoTracking()
        //    .Include(page => page.PageBlocks.OrderBy(block => block.SortOrder))
        //    .SingleOrDefaultAsync(
        //        page =>
        //            page.Slug == slug &&
        //            //page.IsPublished &&
        //            page.IsActive,
        //        cancellationToken);

        //return page is null
        //    ? NotFound()
        //    : View(page);
        return View();
    }
}
