using DatabaseAPI.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAPI.Context.v1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
