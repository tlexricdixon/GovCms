

namespace CmsMvc.Areas.Admin.Models.Content;

/// <summary>
/// Edit model for a region item.
/// </summary>
public class RegionItemModel
{
    /// <summary>
    /// Gets/sets the unique client id.
    /// </summary>
    public string Uid { get; set; } = "uid-" + Math.Abs(Guid.NewGuid().GetHashCode()).ToString();

    /// <summary>
    /// Gets/sets the title if used in a list.
    /// </summary>
    public string Title { get; set; } = "...";

    /// <summary>
    /// Gets/sets if the region is new (added)
    /// </summary>
    public bool IsNew { get; set; }

    /// <summary>
    /// Gets/sets the available fields.
    /// </summary>
    public IList<FieldModel> Fields { get; set; } = new List<FieldModel>();
}
