using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Models;

[Serializable]
public class TranslationSummary
{
    /// <summary>
    /// Gets/sets the group id.
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// Gets/sets if all of the translations is up to date.
    /// </summary>
    public bool IsUpToDate { get; set; }

    /// <summary>
    /// Gets/sets the number of up to content objects.
    /// </summary>
    public int UpToDateCount { get; set; }

    /// <summary>
    /// Gets/sets the total number of translations.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets/sets the translation status items.
    /// </summary>
    public IList<TranslationStatus> Content { get; set; } = new List<TranslationStatus>();
}
