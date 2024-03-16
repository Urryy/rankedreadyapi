using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.User;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Handlers
{
    public class AccountHandler
    {
        public static async Task<IResult> Login(
            [FromBody] LoginUserModel model,
            HttpContext httpContext,
            IUserService srvcUser,
            IConfiguration _configuration
        )
        {
            var authRes = await srvcUser.Login(model);
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(10),
            };

            httpContext.Response.Cookies.Append("X-Refresh-Token", _configuration.GenerateRefreshToken(), cookieOptions);
            return Results.Json(authRes);
        }


        public static async Task<IResult> Register(
            [FromBody] RegisterUserModel model,
            HttpContext httpContext,
            IUserService srvcUser,
            IConfiguration _configuration
        )
        {
            var authRes = await srvcUser.Register(model);
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(10),
            };

            httpContext.Response.Cookies.Append("X-Refresh-Token", _configuration.GenerateRefreshToken(), cookieOptions);
            return Results.Json(authRes);
        }


        [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
        public static async Task<IResult> GetAllUsers(
            IUserService srvcUser)
        {
            var users = await srvcUser.GetUsers();
            return Results.Json(users);
        }


        public static async Task<IResult> GetUserProfile(
            IUserService srvcUser,
            ICurrentUserAccessor accessor)
        {
            var userId = accessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId) && !Guid.TryParse(userId, out var id))
                return Results.Unauthorized();

            var profile = await srvcUser.GetAsync(Guid.Parse(userId!));
            return Results.Json(profile);
        }


        public static async Task<IResult> GetUserProfileById(
            [FromRoute] Guid objectId,
            IUserService srvcUser)
        {
            var user = await srvcUser.GetAsync(objectId);
            return Results.Json(user);
        }


        [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
        public static async Task<IResult> DeleteUser(
            [FromRoute] Guid objectId,
            IUserService srvcUser)
        {
            await srvcUser.DeleteAsync(objectId);
            return Results.Ok();
        }


        public static async Task<IResult> BanUser(
            [FromRoute] Guid objectId,
            IUserService srvcUser)
        {
            await srvcUser.BannedUser(objectId);
            return Results.Ok();
        }


        public static async Task<IResult> UnbanUser(
            [FromRoute] Guid objectId,
            IUserService srvcUser)
        {
            await srvcUser.UnbannedUser(objectId);
            return Results.Ok();
        }


        public static async Task<IResult> ChangePassword(
            [FromQuery] string newPassword,
            [FromQuery] string email,
            IUserService srvcUser)
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(email))
                return Results.BadRequest("Some fields is empty or null");

            await srvcUser.ChangePassword(email, newPassword);
            return Results.Ok();
        }


        public static async Task<IResult> ChangeEmail(
             [FromQuery] string newEmail,
             IUserService srvcUser,
             ICurrentUserAccessor accessor)
        {
            var userId = accessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            await srvcUser.ChangeEmail(userId.ToString(), newEmail);
            return Results.Ok();
        }


        [Permission(Role.Manager, Role.Admin, Role.SuperManager)]
        public static async Task<IResult> ChangePasswordByAdministration(
            [FromQuery] string newPassword,
            [FromQuery] string email,
            IUserService srvcUser)
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(email))
                return Results.BadRequest("Some fields is empty or null");

            await srvcUser.ChangePassword(email, newPassword);
            return Results.Ok();
        }


        [Permission(Role.Manager, Role.Admin, Role.SuperManager)]
        public static async Task<IResult> ChangeEmailByAdministration(
            [FromQuery] string newEmail,
            [FromQuery] Guid userId,
            IUserService srvcUser)
        {
            await srvcUser.ChangeEmail(userId.ToString(), newEmail);
            return Results.Ok();
        }


        public static async Task<IResult> CreateCodeForChangePassword(
            [FromQuery] string email,
            ICodeService srvcCode)
        {
            await srvcCode.SendCodeForChangePassword(email);
            return Results.Ok();
        }


        public static async Task<IResult> ChangePasswordByCode(
            [FromQuery] string code,
            ICodeService srvcCode)
        {
            var isActiveCode = await srvcCode.CheckIsActiveCode(code);
            if (!isActiveCode) return Results.BadRequest("Code is not active");

            await srvcCode.SetNonActiveToCode(code);
            return Results.Ok("Code is valid");
        }


        public static async Task<IResult> GetPurchasedAccounts(
            ICurrentUserAccessor accessor,
            ILeagueLegendAccountService srvcLeagueLegend,
            IValorantAccountService srvcValorant)
        {
            var userId = accessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId) && !Guid.TryParse(userId, out var id))
                return Results.Unauthorized();

            var purchasedAccounts = (await srvcLeagueLegend.GetPurchasedAccounts(Guid.Parse(userId))).ToList();
            purchasedAccounts.AddRange(await srvcValorant.GetPurchasedAccounts(Guid.Parse(userId)));

            return Results.Json(new { purchasedAccounts = purchasedAccounts });
        }


        [Permission(Role.Manager, Role.Admin, Role.SuperManager)]
        public static async Task<IResult> GetPurchasedAccountsByAdministration(
            [FromQuery] Guid objectId,
            ILeagueLegendAccountService srvcLeagueLegend,
            IValorantAccountService srvcValorant)
        {
            var purchasedAccounts = (await srvcLeagueLegend.GetPurchasedAccounts(objectId)).ToList();
            purchasedAccounts.AddRange(await srvcValorant.GetPurchasedAccounts(objectId));

            return Results.Json(new { purchasedAccounts = purchasedAccounts });
        }
    }
}
