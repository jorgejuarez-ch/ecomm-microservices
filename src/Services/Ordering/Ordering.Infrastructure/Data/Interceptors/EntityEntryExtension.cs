using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public static class EntityEntryExtension
    {
        public static bool HasChangesOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(e => e.TargetEntry != null
            && e.TargetEntry.Metadata.IsOwned()
            && (e.TargetEntry.State == EntityState.Added || e.TargetEntry.State == EntityState.Modified));
    }
}
