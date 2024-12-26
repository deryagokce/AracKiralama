using Microsoft.EntityFrameworkCore;

namespace AracKiralama.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category() { Id = 1, Name = "Categori 1", IsActive = true },
               new Category() { Id = 2, Name = "Categori 1", IsActive = true },
               new Category() { Id = 3, Name = "Categori 2", IsActive = true }
);

            modelBuilder.Entity<Car>().HasData(
                   new Car() { Id = 10, Name = "BMW", Price = 1200, IsActive = true, CategoryId = 1},
                   new Car() { Id = 20, Name = "Porsche", Price = 1000, IsActive = true, CategoryId = 2},
                   new Car() { Id = 30, Name = "Toyota", Price = 2000, IsActive = true , CategoryId = 1 }
                );
        }
    }
}