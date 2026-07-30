
namespace Manager.Models;

/// <summary>
/// Base class for blocks that can contain other blocks.
/// </summary>
public abstract class BlockGroup : Block
{
    /// <summary>
    /// Gets/sets the available blocks in this group.
    /// </summary>
    public IList<Block> Items { get; set; } = new List<Block>();
}
