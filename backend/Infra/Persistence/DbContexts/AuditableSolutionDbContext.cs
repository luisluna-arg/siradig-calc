using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Interfaces;

namespace SiradigCalc.Infra.Persistence.DbContexts;

public partial class SolutionDbContext : DbContext, ISolutionDbContext
{
    public override int SaveChanges()
    {
        ApplyDefaultValues();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await ApplyDefaultValues(cancellationToken);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyDefaultValues()
    {
        var now = DateTime.UtcNow;

        var unchagned = ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged).ToArray();

        var entries = ChangeTracker.Entries().ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditable auditableEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    auditableEntity.CreatedAt = now;
                    auditableEntity.UpdatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntity.UpdatedAt = now;
                }
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                ChangeLogs.Add(LogBuilder.BuildLog(entry, now));
            }
        }
    }

    private async Task ApplyDefaultValues(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var entries = ChangeTracker.Entries().ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditable auditableEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    auditableEntity.CreatedAt = now;
                    auditableEntity.UpdatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntity.UpdatedAt = now;
                }
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                await ChangeLogs.AddAsync(LogBuilder.BuildLog(entry, now), cancellationToken);
            }
        }
    }
}
