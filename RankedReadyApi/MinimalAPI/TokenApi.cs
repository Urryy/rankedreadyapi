using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.Token;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class TokenApi
{
    private static string ENDPOINT_V1 = "api/v1/token";

    public static void RegisterTokenApi(this WebApplication app)
    {
        app.MapPost($"{ENDPOINT_V1}/refresh", TokenHandler.RefreshToken)
            .AllowAnonymous()
            .AddEndpointFilter<ValidationFilterAttribute<TokenRefreshModel>>();
    }
}
