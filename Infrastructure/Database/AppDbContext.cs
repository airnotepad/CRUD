using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public AppDbContext(DbContextOptions contextOptions) : base(contextOptions) { }
    }
}