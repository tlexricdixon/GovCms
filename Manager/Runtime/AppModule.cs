using Manager.Contracts;

namespace Manager.Runtime;

public sealed class AppModule : AppDataItem
{
    /// <summary>
    /// Gets/sets the module instance.
    /// </summary>
    public IModule Instance { get; set; }
}
