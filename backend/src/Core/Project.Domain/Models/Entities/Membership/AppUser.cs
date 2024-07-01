using Microsoft.AspNetCore.Identity;

namespace Project.Domain.Models.Entities.Membership
{
    public class AppUser : IdentityUser<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfileImgUrl { get; set; }

    }
}
