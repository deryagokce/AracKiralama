using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AracKiralama.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Seed();
        //}
    }
}