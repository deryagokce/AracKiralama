using Microsoft.AspNetCore.Identity;

namespace AracKiralama.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
    }
}