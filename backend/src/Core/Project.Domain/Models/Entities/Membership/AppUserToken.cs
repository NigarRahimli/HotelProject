﻿using Microsoft.AspNetCore.Identity;

namespace Project.Domain.Models.Entities.Membership
{
    public class AppUserToken : IdentityUserToken<int>
    {
        public bool IsActive { get; set; }
        public DateTime Expired { get; set; }
    }
}
