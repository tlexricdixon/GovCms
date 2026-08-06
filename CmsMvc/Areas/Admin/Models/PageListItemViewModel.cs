namespace CmsMvc.Areas.Admin.Models;

public sealed class PageListItemViewModel
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Slug { get; init; }
    public bool IsPublished { get; init; }
    public DateTime LastModified { get; init; }
}
