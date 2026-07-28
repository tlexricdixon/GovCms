using CmsMvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CmsMvc.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/[controller]")]
public class DashboardController : Controller
{
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new PageCreateViewModel());
    }
}