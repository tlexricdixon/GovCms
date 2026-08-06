
using Manager.Models;

namespace Manager.Contracts;

/// <summary>
/// Interface for content that should be categorized.
/// </summary>
public interface ICategorizedContent
{
    /// <summary>
    /// Gets/sets the optional category.
    /// </summary>
    Taxonomy Category { get; set; }
}
