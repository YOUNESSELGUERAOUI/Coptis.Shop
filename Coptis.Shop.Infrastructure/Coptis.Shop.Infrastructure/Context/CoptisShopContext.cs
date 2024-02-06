using Coptis.Shop.Infrastructure.Entities;
using Coptis.Shop.Infrastructure.Mappers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coptis.Shop.Infrastructure.Context;

public class CoptisShopContext : IdentityDbContext
{
    //public DbSet<Customer> Customers { get; set; }

    public CoptisShopContext(DbContextOptions<CoptisShopContext> options)
        : base(options)
    {            
    }

    public DbSet<TProduct> Products { get; set; }
    public DbSet<TSale> Sales { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TSale>()
            .HasOne(p => p.TUser)
            .WithMany(b => b.TSales)
            .HasForeignKey(p => new { p.UserId });

        builder.Entity<TSale>()
            .HasOne(p => p.TProduct)
            .WithMany(b => b.TSales)
            .HasForeignKey(p => new { p.ProductId });

        base.OnModelCreating(builder);


    }
}
