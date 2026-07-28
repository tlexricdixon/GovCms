namespace CmsModels
{
    public class UserProfile : SyncEntity
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? PasswordHash { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}