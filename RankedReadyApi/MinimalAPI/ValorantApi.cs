using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.Valorant;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class ValorantApi
{
    private static string ENDPOINT_V1 = "api/v1/valorant";

    public static void RegisterValorantApi(this WebApplication app)
    {
        app.MapPost($"{ENDPOINT_V1}", ValorantHandler.GetAvailableAccounts);

        app.MapGet($"{ENDPOINT_V1}", ValorantHandler.GetDetailsAboutAccount);

        app.MapPost($"{ENDPOINT_V1}/all", ValorantHandler.GetAllAccounts)
            .RequireAuthorization();

        app.MapPost($"{ENDPOINT_V1}/create", ValorantHandler.CreateAccount)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<ValorantAccountModel>>();

        app.MapPost($"{ENDPOINT_V1}/update/{{objectId:Guid}}", ValorantHandler.UpdateAccount)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<ValorantAccountModel>>();

        app.MapDelete($"{ENDPOINT_V1}/delete/{{objectId:Guid}}", ValorantHandler.DeleteAccount)
            .RequireAuthorization();
    }
}
