namespace CmsModels
{
    public class Tag : SyncEntity
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }

        public List<PostTag> PostTags { get; set; } = new();
    }
}
