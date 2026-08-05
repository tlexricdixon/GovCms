using Manager.Contracts;

namespace Manager.Models.Extend;

public abstract class Field : IField
{
    public abstract string GetTitle();
}
