namespace CmsModels
{
    public class AnalyticsEntry : SyncEntity
    {
        public DateTime Timestamp { get; set; }
        public string? PageVisited { get; set; }
        public string? Referrer { get; set; }
        public string? UserAgent { get; set; }
        public string? IPAddress { get; set; } // optional / internal use
    }
}
