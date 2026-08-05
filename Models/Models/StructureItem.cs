namespace Manager.Models;

[Serializable]
public abstract class StructureItem<TStructure, T>
where T : StructureItem<TStructure, T>
where TStructure : Structure<TStructure, T>
{
    /// <summary>
    /// Gets/sets the unique id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets/sets the level in the hierarchy.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Gets/sets the child items.
    /// </summary>
    public TStructure Items { get; set; }
}
