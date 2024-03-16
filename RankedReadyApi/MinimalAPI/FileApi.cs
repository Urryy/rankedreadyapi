using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class FileApi
{
    private static string ENDPOINT_V1 = "api/v1/file";
    public static void RegisterFileApi(this WebApplication app)
    {
        app.MapGet($"{ENDPOINT_V1}/skins", FileHandler.GetFileSkin);

        app.MapGet($"{ENDPOINT_V1}/announcement", FileHandler.GetFileAnnouncment);
    }
}
