using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DataAccess.Concretes;

public class Context : DbContext
{
    // DbContextOptions<Context> parametresini alan bir constructor ekliyoruz
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<StoreBill> StoreBills { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Visitor> Visitors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // OperationClaim tablosuna başlangıç verisi eklemek
        modelBuilder.Entity<OperationClaim>().HasData(
            new OperationClaim { Id = Guid.NewGuid(), Name = "Admin" },
            new OperationClaim { Id = Guid.NewGuid(), Name = "Waiter" },
            new OperationClaim { Id = Guid.NewGuid(), Name = "Chef" },
            new OperationClaim { Id = Guid.NewGuid(), Name = "Cashier" }
            // Diğer varsayılan veri eklemeleri burada yapılabilir
        );
    }

}

