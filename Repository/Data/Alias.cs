using Manager.Models;
namespace Repository.Data;

[Serializable]
public sealed class Alias : Manager.Models.Alias
{
    /// <summary>
    /// Gets/sets the site this alias is for.
    /// </summary>
    /// <returns></returns>
    public Site Site { get; set; }
}
