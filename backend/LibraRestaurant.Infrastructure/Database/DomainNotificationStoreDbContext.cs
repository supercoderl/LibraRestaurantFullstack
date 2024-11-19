using LibraRestaurant.Domain.DomainNotifications;
using LibraRestaurant.Infrastructure.Configurations.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Infrastructure.Database;

public class DomainNotificationStoreDbContext : DbContext
{
    public virtual DbSet<StoredDomainNotification> StoredDomainNotifications { get; set; } = null!;

    public DomainNotificationStoreDbContext(DbContextOptions<DomainNotificationStoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new StoredDomainNotificationConfiguration());
    }
}