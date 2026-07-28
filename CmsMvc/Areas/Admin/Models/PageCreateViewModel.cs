using System.ComponentModel.DataAnnotations;

namespace CmsMvc.Areas.Admin.Models;

public sealed class PageCreateViewModel
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [RegularExpression(
        "^[a-z0-9]+(?:-[a-z0-9]+)*$",
        ErrorMessage = "Use lowercase letters, numbers, and hyphens.")]
    public string Slug { get; set; } = string.Empty;
}
