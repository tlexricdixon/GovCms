

using Localization;

namespace Repository.Data;

[Serializable]
public sealed class Alias : Models.Alias
{
    /// <summary>
    /// Gets/sets the site this alias is for.
    /// </summary>
    /// <returns></returns>
    public Site Site { get; set; }
}
