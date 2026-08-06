using Microsoft.AspNetCore.Mvc;

namespace CmsMvc.Areas.Admin.Controllers;

[Area("Admin")]
public sealed class DashboardController : Controller
{
    [HttpGet("/Admin")]
    [HttpGet("/Admin/Dashboard")]
    public IActionResult Index() => View();
}
