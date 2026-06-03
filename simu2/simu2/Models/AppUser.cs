using Microsoft.AspNetCore.Identity;

namespace simu2.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
