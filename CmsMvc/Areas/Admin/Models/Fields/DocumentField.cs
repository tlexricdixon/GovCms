

namespace CmsMvc.Areas.Admin.Models.Fields;

/// <summary>
/// Field for referencing a document asset.
/// </summary>
[FieldType(Name = "Document", Shorthand = "Document", Component = "document-field")]
public class DocumentField : MediaFieldBase<DocumentField>
{
    /// <summary>
    /// Implicit operator for converting a Guid id to a field.
    /// </summary>
    /// <param name="guid">The guid value</param>
    public static implicit operator DocumentField(Guid guid)
    {
        return new DocumentField { Id = guid };
    }

    /// <summary>
    /// Implicit operator for converting a media object to a field.
    /// </summary>
    /// <param name="media">The media object</param>
    public static implicit operator DocumentField(Media media)
    {
        return new DocumentField { Id = media.Id };
    }

    /// <summary>
    /// Impicit operator for converting the field to an url string.
    /// </summary>
    /// <param name="image">The document field</param>
    public static implicit operator string(DocumentField image)
    {
        if (image.Media != null)
        {
            return image.Media.PublicUrl;
        }
        return "";
    }
}
