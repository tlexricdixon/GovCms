

using CmsMvc.Areas.Admin.Data;

namespace CmsMvc.Areas.Admin.Models.Fields;

/// <summary>
/// Field for referencing an archive page.
/// </summary>
[FieldType(Name = "Archive Page", Shorthand = "ArchivePage", Component = "archivepage-field")]
public class ArchivePageField : PageField {}