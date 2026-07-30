

namespace CmsMvc.Areas.Admin.Models.Content;

/// <summary>
/// Generic edit model for blocks.
/// </summary>
public class BlockGenericModel : BlockModel
{
    /// <summary>
    /// Gets/sets the block id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets/sets the type of the block group.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets/sets if the block should be active
    /// part of a group.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets/sets the global fields.
    /// </summary>
    public IList<FieldModel> Model { get; set; } = new List<FieldModel>();
}
