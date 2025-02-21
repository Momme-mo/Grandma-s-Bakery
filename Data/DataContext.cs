using eshop.api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eshop.api.Data;

public class DataContext(DbContextOptions options) : IdentityDbContext(options)
{
  public DbSet<Product> Products { get; set; }
  public DbSet<CustomerOrders> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Address> Addresses { get; set; }
  public DbSet<PostalAddress> PostalAddresses { get; set; }
  public DbSet<AddressType> AddressTypes { get; set; }
  public DbSet<CustomerAddress> CustomerAddresses { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<OrderItem>().HasKey(o => new { o.ProductId, o.SalesOrderId });

    modelBuilder.Entity<CustomerAddress>().HasKey(c => new { c.AddressId, c.CustomerId });

    modelBuilder.Entity<CustomerOrders>().HasKey(c => new { c.CustomerId, c.SalesOrderId });

    modelBuilder.Entity<Product>()
    .HasMany(p => p.OrderItems)
    .WithOne(d => d.Product)
    .HasForeignKey(o => o.ProductId);

    modelBuilder.Entity<Customer>()
    .HasMany(c => c.Orders)
    .WithOne(o => o.Customer)
    .HasForeignKey(o => o.CustomerId);

    base.OnModelCreating(modelBuilder);
  }
}