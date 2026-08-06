using Manager.Models;

namespace CmsMvc.Models;

public sealed class PageDisplayViewModel
{
    public required string Title { get; init; }
    public required string Slug { get; init; }
    public required IReadOnlyList<PageBlockDisplayViewModel> Blocks { get; init; }
}

public sealed class PageBlockDisplayViewModel
{
    public BlockType BlockType { get; init; }
    public string? HeadingText { get; init; }
    public int? HeadingLevel { get; init; }
    public string? ParagraphText { get; init; }
    public string? LinkText { get; init; }
    public string? LinkUrl { get; init; }
    public bool OpenInNewWindow { get; init; }
}
