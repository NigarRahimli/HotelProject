using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Project.Infrastructure.Extensions
{
    static public partial class Extension
    {
        public static int GetUserIdExtension(this HttpContext httpContext)
        {
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new UnauthorizedAccessException("Unable to retrieve userId from token.");
            }

            return userId;
        }
    }
}
