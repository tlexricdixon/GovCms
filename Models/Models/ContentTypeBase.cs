using Manager.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

/// <summary>
/// Base class for templated content types.
/// </summary>
[Serializable]
public abstract class ContentTypeBase : ITypeModel
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
    /// Gets/sets the optional title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets/sets the optional description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets/sets the available regions.
    /// </summary>
    public IList<ContentTypeRegion> Regions { get; set; } = new List<ContentTypeRegion>();

    /// <summary>
    /// Gets/sets the optional routes.
    /// </summary>
    public IList<ContentTypeRoute> Routes { get; set; } = new List<ContentTypeRoute>();

    /// <summary>
    /// Gets/sets the optional custom editors.
    /// </summary>
    public IList<ContentTypeEditor> CustomEditors { get; set; } = new List<ContentTypeEditor>();
}
