using AracKiralama.Models;
using AracKiralama.ViewModels;
using AutoMapper;

namespace AracKiralama.Repositories
{
        public class CarRepository : GenericRepository<Car>
        {
            public CarRepository(AppDbContext context) : base(context)
            {
            }
        }
}