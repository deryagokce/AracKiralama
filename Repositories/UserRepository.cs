using AracKiralama.Models;

namespace AracKiralama.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}