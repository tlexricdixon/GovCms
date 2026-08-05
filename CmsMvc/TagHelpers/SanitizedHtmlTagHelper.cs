using CmsMvc.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CmsMvc.TagHelpers;

/// <summary>
/// Tag helper for rendering safely sanitized HTML content.
/// Usage: <sanitized-html content="@Model.HtmlContent" />
/// </summary>
[HtmlTargetElement("sanitized-html")]
public class SanitizedHtmlTagHelper : TagHelper
{
    private readonly IHtmlSanitizer _sanitizer;

    [HtmlAttributeName("content")]
    public string? Content { get; set; }

    public SanitizedHtmlTagHelper(IHtmlSanitizer sanitizer)
    {
        _sanitizer = sanitizer;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null; // Don't render the tag itself
        output.Content.SetHtmlContent(_sanitizer.Sanitize(Content));
    }
}