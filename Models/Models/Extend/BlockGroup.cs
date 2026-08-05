namespace Manager.Models.Extend;

public abstract class BlockGroup : Block
{
    /// <summary>
    /// Gets/sets the available blocks in this group.
    /// </summary>
    public IList<Block> Items { get; set; } = new List<Block>();
}
