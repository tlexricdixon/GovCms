

using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

[Serializable]
public abstract class Comment : SyncEntity
{
    public int PostId { get; set; }
    public required Post Post { get; set; }
    /// <summary>
    /// Gets/sets the comment body.
    /// </summary>
    [Required]
    public string Body { get; set; } = string.Empty;

    public required string AuthorName { get; set; }
    public required string AuthorEmail { get; set; }
    public required string Content { get; set; }
    public DateTime SubmittedAt { get; set; }
    public bool IsApproved { get; set; }
}
