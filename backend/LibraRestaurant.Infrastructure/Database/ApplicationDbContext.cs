using System.Linq;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LibraRestaurant.Infrastructure.Database;

public partial class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<MenuItem> MenuItems { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<OrderHeader> OrderHeaders { get; set; } = null!;
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<OrderLine> OrderLines { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<Ward> Wards { get; set; } = null!;
    public DbSet<PaymentHistory> PaymentHistories { get; set; } = null!;
    public DbSet<CategoryItem> CategoryItems { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<EmployeeRole> EmployeeRoles { get; set; } = null!;
    public DbSet<Token> Tokens { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<OrderLog> OrderLogs { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<DiscountType> DiscountTypes { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            if (entity.ClrType.GetProperty(DbContextUtility.IsDeletedProperty) is not null)
            {
                builder.Entity(entity.ClrType)
                    .HasQueryFilter(DbContextUtility.GetIsDeletedRestriction(entity.ClrType));
            }
        }
        builder.Entity<OrderLine>(entry =>
        {
            entry.ToTable("OrderLines", tb => tb.HasTrigger("trg_OrderLines_AfterUpdate"));
        });

        base.OnModelCreating(builder);

        ApplyConfigurations(builder);

        // Make referential delete behaviour restrict instead of cascade for everything
        foreach (var relationship in builder.Model.GetEntityTypes()
                     .SelectMany(x => x.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private static void ApplyConfigurations(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmployeeConfiguration());
        builder.ApplyConfiguration(new MenuItemConfiguration());
        builder.ApplyConfiguration(new MenuConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CurrencyConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new StoreConfiguration());   
        builder.ApplyConfiguration(new ReservationConfiguration());
        builder.ApplyConfiguration(new OrderLineConfiguration());
        builder.ApplyConfiguration(new PaymentMethodConfiguration());
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new DistrictConfiguration());
        builder.ApplyConfiguration(new WardConfiguration());
        builder.ApplyConfiguration(new PaymentHistoryConfiguration());
        builder.ApplyConfiguration(new CategoryItemConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new EmployeeRoleConfiguration());
        builder.ApplyConfiguration(new TokenConfiguration());
        builder.ApplyConfiguration(new MessageConfiguration());
        builder.ApplyConfiguration(new OrderLogConfiguration());
        builder.ApplyConfiguration(new DiscountConfiguration());
        builder.ApplyConfiguration(new DiscountTypeConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
    }
}