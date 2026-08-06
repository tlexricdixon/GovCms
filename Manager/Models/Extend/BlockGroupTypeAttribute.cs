namespace Manager.Models.Extend;

using System;

[AttributeUsage(AttributeTargets.Class)]
public class BlockGroupTypeAttribute : BlockTypeAttribute
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public BlockGroupTypeAttribute()
    {
        _component = null;
    }

    /// <summary>
    /// Gets/sets how the blocks inside the group should be
    /// displayed in the manager interface.
    /// </summary>
    public BlockDisplayMode Display { get; set; } = BlockDisplayMode.MasterDetail;
}
