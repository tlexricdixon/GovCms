using CmsModels;
using System.ComponentModel.DataAnnotations;

namespace CmsMvc.Areas.Admin.Models;

public sealed class PageEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [RegularExpression(
        "^[a-z0-9]+(?:-[a-z0-9]+)*$",
        ErrorMessage = "Use lowercase letters, numbers, and hyphens.")]
    public string Slug { get; set; } = string.Empty;

    public bool IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime LastModified { get; set; }

    public BlockType NewBlockType { get; set; }

    public IReadOnlyList<PageBlock> PageBlocks { get; set; }
        = Array.Empty<PageBlock>();
}
