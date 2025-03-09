using eshop.api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eshop.api.Data;

public class DataContext : IdentityDbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  public DbSet<Product> Products { get; set; }
  public DbSet<CustomerOrder> CustomerOrders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Address> Addresses { get; set; }
  public DbSet<PostalAddress> PostalAddresses { get; set; }
  public DbSet<AddressType> AddressTypes { get; set; }
  public DbSet<CustomerAddress> CustomerAddresses { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CustomerOrder>()
    .HasOne(o => o.Customer)
    .WithMany(c => c.CustomerOrders)
    .HasForeignKey(o => o.CustomerId);

    modelBuilder.Entity<CustomerOrder>()
    .HasMany(o => o.OrderItems)
    .WithOne(oi => oi.CustomerOrder)
    .HasForeignKey(oi => oi.OrderId);

    modelBuilder.Entity<OrderItem>()
    .HasOne(o => o.Product)
    .WithMany(o => o.OrderItems)
    .HasForeignKey(o => o.ProductId);

    modelBuilder.Entity<CustomerAddress>()
    .HasKey(c => new { c.AddressId, c.CustomerId });

    base.OnModelCreating(modelBuilder);
  }
}