using Manager.Contracts;

namespace Manager.Models;

/// <summary>
/// Simple content class for querying large sets of
/// data without loading regions.
/// </summary>
[Serializable]
public class ContentInfo : GenericContent, IContentInfo { }
