namespace Manager.Models;

[Serializable]
public class TranslationStatus
{
    [Serializable]
    public class TranslationStatusItem
    {
        /// <summary>
        /// Gets/sets the languge id.
        /// </summary>
        public Guid LanguageId { get; set; }

        /// <summary>
        /// Gets/sets the language title.
        /// </summary>
        public string LanguageTitle { get; set; }

        /// <summary>
        /// Gets/sets if the language is up to date with the
        /// default master language.
        /// </summary>
        public bool IsUpToDate { get; set; }
    }

    /// <summary>
    /// Gets/sets the unique content id.
    /// </summary>
    public Guid ContentId { get; set; }

    /// <summary>
    /// Gets/sets if all of the translations is up to date.
    /// </summary>
    public bool IsUpToDate { get; set; }

    /// <summary>
    /// Gets/sets the number of up to date translations.
    /// </summary>
    public int UpToDateCount { get; set; }

    /// <summary>
    /// Gets/sets the total number of translations.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets/sets the status items for the available languages.
    /// </summary>
    public IList<TranslationStatusItem> Translations { get; set; } = new List<TranslationStatusItem>();
}

