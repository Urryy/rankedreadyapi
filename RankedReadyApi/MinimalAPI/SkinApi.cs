using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.Skin;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class SkinApi
{
    private static string ENDPOINT_V1 = "api/v1/skin";

    public static void RegisterSkinApi(this WebApplication app)
    {
        app.MapGet($"{ENDPOINT_V1}/skins", SkinHandler.GetSkins);

        app.MapGet($"{ENDPOINT_V1}/skins/{{objectId:Guid}}", SkinHandler.GetSkin);

        app.MapPost($"{ENDPOINT_V1}/create", SkinHandler.CreateSkin)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<SkinModel>>();

        app.MapPost($"{ENDPOINT_V1}/delete{{objectId:Guid}}", SkinHandler.DeleteSkin)
            .RequireAuthorization();
    }
}
