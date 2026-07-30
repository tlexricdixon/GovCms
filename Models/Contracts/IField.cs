namespace Manager.Contracts;

/// <summary>
/// Interface for fields.
/// </summary>
public interface IField
{
    /// <summary>
    /// Gets the list item title if this field is used in
    /// a collection regions.
    /// </summary>
    string GetTitle();
}
