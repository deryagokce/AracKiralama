using Microsoft.EntityFrameworkCore;

namespace AracKiralama.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(
                   new Car() { Id = 1, Name = "BMW", Price = 1200, IsActive = true},
                   new Car() { Id = 2, Name = "Porsche", Price = 1000, IsActive = true },
                   new Car() { Id = 3, Name = "Toyota", Price = 2000, IsActive = true }
                );
        }
    }
}