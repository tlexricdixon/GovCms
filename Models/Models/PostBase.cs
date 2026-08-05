using Manager.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

[Serializable]
public abstract class PostBase : RoutedContentBase, ICategorizedContent, ITaggedContent
{
    /// <summary>
    /// Gets/sets the blog page id.
    /// </summary>
    [Required]
    public Guid BlogId { get; set; }

    /// <summary>
    /// Gets/sets the category.
    /// </summary>
    [Required]
    public Taxonomy Category { get; set; }

    /// <summary>
    /// Gets/sets the available tags.
    /// </summary>
    public IList<Taxonomy> Tags { get; set; } = new List<Taxonomy>();
}
