using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

[Serializable]
public sealed class PageBlock : BaseModel
{
    public int PageId { get; set; }

    public Page Page { get; set; } = null!;

    public int SortOrder { get; set; }

    public BlockType BlockType { get; set; }

    [StringLength(300)]
    public string? HeadingText { get; set; }

    public int? HeadingLevel { get; set; }

    [StringLength(8000)]
    public string? ParagraphText { get; set; }

    [StringLength(300)]
    public string? LinkText { get; set; }

    [StringLength(2048)]
    public string? LinkUrl { get; set; }

    public bool OpenInNewWindow { get; set; }
}
