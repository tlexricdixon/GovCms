namespace CmsModels;
public abstract class SyncEntity
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
    public string? ModifiedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public bool NeedsSync { get; set; } = true;
}