namespace CmsModels
{
    public class Page : SyncEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }
        public ICollection<PageBlock> PageBlocks { get; set; } = new List<PageBlock>();
    }
}
