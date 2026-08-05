using Ganss.Xss;
namespace CmsMvc.Services;

/// <summary>
/// Service for sanitizing HTML content to prevent XSS attacks.
/// Provides a safe way to render user-submitted HTML while removing dangerous elements.
/// </summary>
public interface IHtmlSanitizer
{
    /// <summary>
    /// Sanitizes HTML content by removing potentially dangerous elements and attributes.
    /// </summary>
    /// <param name="html">The HTML content to sanitize</param>
    /// <returns>Safe HTML content</returns>
    string Sanitize(string? html);
}

/// <summary>
/// Implementation of HTML sanitizer using HtmlSanitizer library.
/// </summary>
public sealed class HtmlSanitizationService : IHtmlSanitizer
{
    private readonly HtmlSanitizer _sanitizer;

    public HtmlSanitizationService()
    {
        _sanitizer = new HtmlSanitizer();
        ConfigureSanitizer();
    }

    /// <summary>
    /// Configures the sanitizer with allowed tags and attributes.
    /// </summary>
    private void ConfigureSanitizer()
    {
        // Allow common formatting tags
        var allowedTags = new[]
        {
            "p", "br", "strong", "b", "em", "i", "u", "s",
            "h1", "h2", "h3", "h4", "h5", "h6",
            "ul", "ol", "li",
            "a", "img",
            "blockquote", "code", "pre",
            "hr", "table", "thead", "tbody", "tr", "th", "td",
            "div", "span", "section", "article"
        };

        // Allow specific attributes
        var allowedAttributes = new Dictionary<string, string[]>
        {
            { "a", new[] { "href", "title", "target", "rel" } },
            { "img", new[] { "src", "alt", "title", "width", "height" } },
            { "table", new[] { "border", "cellpadding", "cellspacing" } },
            { "td", new[] { "colspan", "rowspan" } },
            { "th", new[] { "colspan", "rowspan" } },
            { "*", new[] { "class", "id", "style" } } // Allow class/id/style on all tags
        };

        // Configure allowed tags
        _sanitizer.AllowedTags.Clear();
        foreach (var tag in allowedTags)
        {
            _sanitizer.AllowedTags.Add(tag);
        }

        // Configure allowed attributes
        _sanitizer.AllowedAttributes.Clear();
        foreach (var attr in allowedAttributes)
        {
            foreach (var allowedAttr in attr.Value)
            {
                _sanitizer.AllowedAttributes.Add($"{attr.Key}@{allowedAttr}");
            }
        }

        // Remove dangerous protocols
        _sanitizer.AllowedSchemes.Clear();
        _sanitizer.AllowedSchemes.Add("http");
        _sanitizer.AllowedSchemes.Add("https");
        _sanitizer.AllowedSchemes.Add("mailto");
    }

    public string Sanitize(string? html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        return _sanitizer.Sanitize(html);
    }
}