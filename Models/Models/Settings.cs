namespace Manager.Models
{
    public class Settings : SyncEntity
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
