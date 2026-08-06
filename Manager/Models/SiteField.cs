

namespace Manager.Models;

[Serializable]
public sealed class SiteField : ContentFieldBase
{
    /// <summary>
    /// Gets/sets the site id.
    /// </summary>
    public Guid SiteId { get; set; }

    /// <summary>
    /// Gets/sets the site.
    /// </summary>
    public Site Site { get; set; }
}
