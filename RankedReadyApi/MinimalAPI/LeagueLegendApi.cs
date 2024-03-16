using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.LeagueLegend;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class LeagueLegendApi
{
    private static string ENDPOINT_V1 = "api/v1/leaguelegendaccount";

    public static void RegisterLeagueLegendApi(this WebApplication app)
    {
        app.MapPost($"{ENDPOINT_V1}", LeagueLegendHandler.GetAvailableAccounts);

        app.MapGet($"{ENDPOINT_V1}", LeagueLegendHandler.GetDetailsAboutAccount);

        app.MapPost($"{ENDPOINT_V1}/all", LeagueLegendHandler.GetAllAccounts)
            .RequireAuthorization();

        app.MapPost($"{ENDPOINT_V1}/create", LeagueLegendHandler.CreateAccount)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<LeagueLegendAccountModel>>();

        app.MapPost($"{ENDPOINT_V1}/update/{{objectId:Guid}}", LeagueLegendHandler.UpdateAccount)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<LeagueLegendAccountModel>>();

        app.MapDelete($"{ENDPOINT_V1}/delete/{{objectId:Guid}}", LeagueLegendHandler.DeleteAccount)
            .RequireAuthorization();
    }
}
