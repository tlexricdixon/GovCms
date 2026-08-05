

using Manager.Contracts;

namespace CmsMvc.Areas.Admin.Models.Content;

/// <summary>
/// Edit model for a field.
/// </summary>
public class FieldModel
{
    /// <summary>
    /// Gets/sets the field model.
    /// </summary>
    public IField Model { get; set; }

    /// <summary>
    /// Gets/sets the meta information.
    /// </summary>
    public FieldMeta Meta { get; set; }
}
