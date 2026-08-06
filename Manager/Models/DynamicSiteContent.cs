
using Manager.Contracts;
using System.Dynamic;

namespace Manager.Models;

/// <summary>
/// Dynamic page model.
/// </summary>
[Serializable]
public class DynamicSiteContent : SiteContent<DynamicSiteContent>, IDynamicContent
{
    /// <summary>
    /// Gets/sets the regions.
    /// </summary>
    public dynamic Regions { get; set; } = new ExpandoObject();
}
