using CmsModels;
using Microsoft.EntityFrameworkCore;

namespace DbContexts;

public class LocalDbContext(DbContextOptions<LocalDbContext> options) : DbContext(options)
{
    public DbSet<AnalyticsEntry> AnalyticsEntries => Set<AnalyticsEntry>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<ContactFormSubmission> ContactForm => Set<ContactFormSubmission>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostTag> PostTags => Set<PostTag>();
    public DbSet<Settings> Settings => Set<Settings>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<UserProfile> Users => Set<UserProfile>();
    public DbSet<PageBlock> PageBlocks => Set<PageBlock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new PostTagConfiguration());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Category>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Comment>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Page>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Tag>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<UserProfile>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<Settings>());
        modelBuilder.ApplyConfiguration(new SyncEntityConfiguration<ContactFormSubmission>());
        modelBuilder.ApplyConfiguration(new PageBlockConfiguration());
    }

    public override int SaveChanges()
    {
        UpdateModifiedDates();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateModifiedDates();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateModifiedDates()
    {
        foreach (var entry in ChangeTracker.Entries<SyncEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}
