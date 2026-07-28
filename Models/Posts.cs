namespace CmsModels;
public class Post : SyncEntity
{
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public required string Excerpt { get; set; }
    public DateTime PublishedAt { get; set; }
    public bool IsPublished { get; set; }
    public required string Author { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public List<PostTag> PostTags { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}

