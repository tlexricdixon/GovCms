using Manager.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

/// <summary>
/// Class for defining a content group.
/// </summary>
[Serializable]
public class ContentGroup : ITypeModel
{
    /// <summary>
    /// Gets/sets the unique id.
    /// </summary>
    [Required]
    [StringLength(64)]
    public string Id { get; set; }

    /// <summary>
    /// Gets/sets the CLR type of the content model.
    /// </summary>
    [StringLength(255)]
    public string CLRType { get; set; }

    /// <summary>
    /// Gets/sets the display title.
    /// </summary>
    [Required]
    [StringLength(128)]
    public string Title { get; set; }

    /// <summary>
    /// Gets/set the icon css.
    /// </summary>
    [StringLength(64)]
    public string Icon { get; set; }

    /// <summary>
    /// Gets/sets if the content group should be hidden from the
    /// menu or not. The default value is false.
    /// </summary>
    public bool IsHidden { get; set; }
}
