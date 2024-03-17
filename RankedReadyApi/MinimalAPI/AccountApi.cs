using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.User;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI
{
    public static class AccountApi
    {
        private static string ENDPOINT_V1 = "/api/v1/account";
        public static void RegisterAccountApi(this WebApplication app)
        {
            app.MapPost($"{ENDPOINT_V1}/login", AccountHandler.Login)
                .AddEndpointFilter<ValidationFilterAttribute<LoginUserModel>>();

            app.MapPost($"{ENDPOINT_V1}/register", AccountHandler.Register)
                .AddEndpointFilter<ValidationFilterAttribute<RegisterUserModel>>();

            app.MapGet($"{ENDPOINT_V1}/all", AccountHandler.GetAllUsers)
                .RequireAuthorization();

            app.MapGet($"{ENDPOINT_V1}/profile", AccountHandler.GetUserProfile)
                .RequireAuthorization();

            app.MapGet($"{ENDPOINT_V1}/profile/{{objectId:Guid}}", AccountHandler.GetUserProfileById)
                .RequireAuthorization();

            app.MapDelete($"{ENDPOINT_V1}/delete/{{objectId:Guid}}", AccountHandler.DeleteUser)
                .RequireAuthorization();

            app.MapPut($"{ENDPOINT_V1}/ban/{{objectId:Guid}}", AccountHandler.BanUser)
                .RequireAuthorization();

            app.MapPut($"{ENDPOINT_V1}/unban/{{objectId:Guid}}", AccountHandler.UnbanUser)
                .RequireAuthorization();

            app.MapPut($"{ENDPOINT_V1}/changepwd", AccountHandler.ChangePassword);

            app.MapPut($"{ENDPOINT_V1}/changeemail", AccountHandler.ChangeEmail)
                .RequireAuthorization();

            app.MapPut($"{ENDPOINT_V1}/changepwd/byadmin", AccountHandler.ChangePasswordByAdministration)
                .RequireAuthorization();

            app.MapPut($"{ENDPOINT_V1}/changeemail/byadmin", AccountHandler.ChangeEmailByAdministration)
                .RequireAuthorization();

            app.MapGet($"{ENDPOINT_V1}/changepwd/send/code", AccountHandler.CreateCodeForChangePassword);

            app.MapGet($"{ENDPOINT_V1}/changepwd/by/code", AccountHandler.ChangePasswordByCode);

            app.MapGet($"{ENDPOINT_V1}/purchased/accounts", AccountHandler.GetPurchasedAccounts)
                .RequireAuthorization();

            app.MapGet($"{ENDPOINT_V1}/purchased/accounts/admin", AccountHandler.GetPurchasedAccountsByAdministration)
                .RequireAuthorization();
        }
    }
}
