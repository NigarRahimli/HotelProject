using Microsoft.AspNetCore.Identity;

namespace Project.Domain.Models.Entities.Membership
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
