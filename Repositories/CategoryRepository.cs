using AracKiralama.Models;
using AracKiralama.ViewModels;
using AutoMapper;

namespace AracKiralama.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}