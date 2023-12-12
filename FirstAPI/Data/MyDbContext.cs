using FirstAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FirstAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions dbcontextOptions) : base(dbcontextOptions)
        {

        }


        public DbSet<Region> Regions { get; set; }
        public DbSet<Products> Products { get; set; }

        // Inside your DbContext class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
