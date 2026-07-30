namespace Manager.Models;

public class PageBlock : SyncEntity
{
    public int PageId { get; set; }

    public Page? Page { get; set; } = null;

    public int SortOrder { get; set; }

    public BlockType BlockType { get; set; }

    public string? HeadingText { get; set; }

    public int? HeadingLevel { get; set; }

    public string? ParagraphText { get; set; }

    public string? LinkText { get; set; }

    public string? LinkUrl { get; set; }

    public bool OpenInNewWindow { get; set; }
}
