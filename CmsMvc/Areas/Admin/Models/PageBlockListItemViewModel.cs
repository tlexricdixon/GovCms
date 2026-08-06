using Manager.Models;

namespace CmsMvc.Areas.Admin.Models;

public sealed class PageBlockListItemViewModel
{
    public int Id { get; init; }
    public int SortOrder { get; init; }
    public BlockType BlockType { get; init; }
    public string? Summary { get; init; }
}
