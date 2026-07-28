namespace CmsModels
{
    public class Comment : SyncEntity
    {
        public int PostId { get; set; }
        public required Post Post { get; set; }

        public required string AuthorName { get; set; }
        public required string AuthorEmail { get; set; }
        public required string Content { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsApproved { get; set; }
    }
}
