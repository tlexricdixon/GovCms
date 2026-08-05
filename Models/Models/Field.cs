using Manager.Contracts;

namespace Manager.Models;

/// <summary>
/// Base class for fields.
/// </summary>
public abstract class Field : IField
{
    /// <inheritdoc />
    public abstract string GetTitle();
}
