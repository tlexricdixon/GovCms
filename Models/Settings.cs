namespace CmsModels
{
    public class Settings : SyncEntity
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
