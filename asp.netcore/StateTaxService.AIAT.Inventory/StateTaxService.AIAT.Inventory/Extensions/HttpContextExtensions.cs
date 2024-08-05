using StateTaxService.AIAT.Inventory.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StateTaxService.AIAT.Inventory.Extensions;

public static class HttpContextExtensions
{
    public static string GetJwtToken(this HttpContext httpContext)
    {
        return httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    }

    public static Guid GetUserIdFromToken(this HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException(Constants.AuthorizationTokenNotFound);
        }
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "jti");

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            return userId;
        }

        return Guid.Empty;
    }

    public static List<string> GetPermissionGroupsFromToken(this HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException(Constants.AuthorizationTokenNotFound);
        }
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var permissionGroupsClaims = jwtSecurityToken.Claims.Where(claim => claim.Type == "PermissionGroup").ToList();

        var permissionGroups = new List<string>();

        foreach (var claim in permissionGroupsClaims)
        {
            if (!string.IsNullOrEmpty(claim.Value))
            {
                permissionGroups.AddRange(claim.Value.Split(',').Select(pg => pg.Trim()));
            }
        }

        return permissionGroups;
    }
}
