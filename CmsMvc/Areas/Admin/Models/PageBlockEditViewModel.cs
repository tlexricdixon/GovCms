using Manager.Models;
using System.ComponentModel.DataAnnotations;

namespace CmsMvc.Areas.Admin.Models;

public sealed class PageBlockEditViewModel : IValidatableObject
{
    public int Id { get; set; }
    public int PageId { get; set; }
    public BlockType BlockType { get; set; }

    [StringLength(300)]
    public string? HeadingText { get; set; }

    [Range(2, 6)]
    public int? HeadingLevel { get; set; }

    [StringLength(8000)]
    public string? ParagraphText { get; set; }

    [StringLength(300)]
    public string? LinkText { get; set; }

    [StringLength(2048)]
    public string? LinkUrl { get; set; }

    public bool OpenInNewWindow { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        switch (BlockType)
        {
            case BlockType.Heading:
                if (string.IsNullOrWhiteSpace(HeadingText))
                {
                    yield return new ValidationResult("Heading text is required.", [nameof(HeadingText)]);
                }

                if (HeadingLevel is < 2 or > 6)
                {
                    yield return new ValidationResult("Heading level must be between 2 and 6.", [nameof(HeadingLevel)]);
                }
                break;

            case BlockType.Paragraph:
                if (string.IsNullOrWhiteSpace(ParagraphText))
                {
                    yield return new ValidationResult("Paragraph text is required.", [nameof(ParagraphText)]);
                }
                break;

            case BlockType.Link:
                if (string.IsNullOrWhiteSpace(LinkText))
                {
                    yield return new ValidationResult("Link text is required.", [nameof(LinkText)]);
                }

                if (!IsAllowedLink(LinkUrl))
                {
                    yield return new ValidationResult(
                        "Enter a local path or an HTTP, HTTPS, or mailto URL.",
                        [nameof(LinkUrl)]);
                }
                break;

            default:
                yield return new ValidationResult("The selected block type is not supported.", [nameof(BlockType)]);
                break;
        }
    }

    private static bool IsAllowedLink(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (value.StartsWith('/') && !value.StartsWith("//"))
        {
            return true;
        }

        return Uri.TryCreate(value, UriKind.Absolute, out var uri) &&
            uri.Scheme is "http" or "https" or "mailto";
    }
}
