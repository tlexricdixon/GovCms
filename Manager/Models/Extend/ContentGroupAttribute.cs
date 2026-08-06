using Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Models.Extend;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ContentGroupAttribute : Attribute
{
    private string _title;

    /// <summary>
    /// Gets/sets the unique id.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets/sets the title.
    /// </summary>
    public string Title
    {
        get => _title;
        set
        {
            _title = value;

            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = Utils.GenerateInternalId(value);
            }
        }
    }

    /// <summary>
    /// Gets/set the icon css.
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Gets/sets if the content group should be hidden from the
    /// menu or not. The default value is false.
    /// </summary>
    public bool IsHidden { get; set; }
}

