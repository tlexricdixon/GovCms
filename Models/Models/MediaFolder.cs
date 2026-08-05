using System.ComponentModel.DataAnnotations;

namespace Manager.Models;

[Serializable]
public class MediaFolder
{
    /// <summary>
    /// Gets/sets the unique id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets/sets the optional parent id.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Gets/sets the folder name.
    /// </summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    /// <summary>
    /// Gets/sets the optional description.
    /// </summary>
    [StringLength(512)]
    public string Description { get; set; }

    /// <summary>
    /// Gets/sets the created date.
    /// </summary>
    public DateTime Created { get; set; }
}

[Serializable]
public class MediaFolderSimple
{
    /// <summary>
    /// Gets/sets the unique id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets/sets the folder name.
    /// </summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; }
}
