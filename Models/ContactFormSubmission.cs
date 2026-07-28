namespace CmsModels
{
    public class ContactFormSubmission : SyncEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Subject { get; set; }
        public required string Message { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsHandled { get; set; }
    }
}
