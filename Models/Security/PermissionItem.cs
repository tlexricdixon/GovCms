using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Security;

public class PermissionItem
{
    /// <summary>
    /// The name of the claim.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The display title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets/sets the optional category for grouping.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets/sets if this is an internal permissions used
    /// by Piranha.
    /// </summary>
    public bool IsInternal { get; set; }
}

