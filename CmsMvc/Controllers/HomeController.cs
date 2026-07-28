using CmsModels;
using Microsoft.AspNetCore.Mvc;

namespace CmsMvc.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index()
    {
        var page = new Page
        {
            Title = "ISP CMS MVC Prototype",
            Slug = "home",
            //Content = "The existing CMS models are now rendering through a clean .NET 10 MVC frontend without Html.Raw.",
            IsPublished = true,
            PublishedAt = DateTime.UtcNow
        };

        return View(page);
    }
}
