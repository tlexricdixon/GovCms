
using Manager.Models;
using Manager.Models.Extend;

namespace Manager.Contracts;

public interface IBlockContent
{
    /// <summary>
    /// Gets/sets the blocks.
    /// </summary>
    IList<Block> Blocks { get; set; }
}
