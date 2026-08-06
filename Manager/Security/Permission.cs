namespace Manager.Security;

public static class Permission
{
    public const string PagePreview = "PiranhaPagePreview";
    public const string PostPreview = "PiranhaPostPreview";

    public static string[] All()
    {
        return new[]
        {
            PagePreview,
            PostPreview
        };
    }
}
