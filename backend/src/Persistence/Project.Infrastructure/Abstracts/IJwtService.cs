using System.Security.Claims;

namespace Project.Infrastructure.Abstracts;

public interface IJwtService
{

    string GenerateAccessToken(ClaimsPrincipal principal);
    string GenerateRefreshToken(string accessToken);
}