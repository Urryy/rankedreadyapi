using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Models.Token;
using RankedReadyApi.DataAccess.Extensions;
using System.Security.Claims;

namespace RankedReadyApi.Handlers;

public static class TokenHandler
{
    public static async Task<IResult> RefreshToken([FromBody] TokenRefreshModel model,
        HttpContext httpContext,
        IConfiguration configuration,
        IUserService srvcUser,
        ITokenService tokenService)
    {
        string refreshToken = httpContext.Request.Cookies["X-Refresh-Token"];
        if (string.IsNullOrEmpty(refreshToken))
            return Results.BadRequest("Invalid client request with refresh token");

        string accessToken = model.AccessToken;
        var principal = configuration.GetPrincipalFromExpiredToken(accessToken);
        var userIdentity = principal?.Claims?
            .FirstOrDefault(usr => usr.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdentity) || !Guid.TryParse(userIdentity, out var userId))
            return Results.BadRequest("Invalid user id from expiration token");

        var user = await srvcUser.GetUserById(userId);

        var newAccessToken = await tokenService.CreateToken(user);
        var cookieOptions = new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        httpContext.Response.Cookies.Append("X-Refresh-Token", configuration.GenerateRefreshToken(), cookieOptions);

        return Results.Json(new { AccessToken = newAccessToken });
    }
}
