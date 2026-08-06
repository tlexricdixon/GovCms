using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

/// <summary>
/// Common identity, audit, and lifecycle fields for persisted CMS models.
/// </summary>
public abstract class BaseModel
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    public string? CreatedBy { get; set; }

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; } = true;

    [Timestamp]
    public byte[] RowVersion { get; set; } = [];
}
