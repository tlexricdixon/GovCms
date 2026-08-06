using Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace DbContexts;

public class LocalDbContext(DbContextOptions<LocalDbContext> options) : DbContext(options)
{
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<PageBlock> PageBlocks => Set<PageBlock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PageConfiguration());
        modelBuilder.ApplyConfiguration(new PageBlockConfiguration());
    }

    public override int SaveChanges()
    {
        ApplyAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditFields()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
            }

            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.LastModified = now;
            }
        }
    }
}
