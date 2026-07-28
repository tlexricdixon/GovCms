namespace CmsModels
{
    public class Category : SyncEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}
