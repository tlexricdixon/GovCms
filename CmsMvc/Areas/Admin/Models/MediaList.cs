namespace CmsMvc.Areas.Admin.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

    [Authorize(Policy = Permission.Media)]
    public class MediaListViewModel : PageModel
    {
    }
