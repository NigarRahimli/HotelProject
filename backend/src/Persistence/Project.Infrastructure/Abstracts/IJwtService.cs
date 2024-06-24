using System.Security.Claims;

namespace Project.Infrastructure.Abstracts;

public interface IJwtService
{
    int? GetPrincipialId();

    string GenerateAccessToken(ClaimsPrincipal principal);
    string GenerateRefreshToken(string accessToken);
}