using Microsoft.EntityFrameworkCore; 
using OrderApi.Models; 
 
namespace OrderApi.Data; 
 
public class AppDbContext : DbContext 
 { 
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
 
     public DbSet<Order> Orders { get; set; } 
     public DbSet<Customer> Customers { get; set; }
     public DbSet<Product> Products { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<Order>()
             .HasOne(o => o.Customer)
             .WithMany(c => c.Orders)
             .HasForeignKey(o => o.CustomerId);

         modelBuilder.Entity<Order>()
             .HasOne(o => o.Product)
             .WithMany()
             .HasForeignKey(o => o.ProductId);
     }
 } 
