using RankedReadyApi.MinimalAPI;

namespace RankedReadyApi.Extension;

public static class EndpointsExtension
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.RegisterAccountApi();
        app.RegisterAnnouncementApi();
        app.RegisterSkinApi();
        app.RegisterStatisticApi();
        app.RegisterSupportTicketApi();
        app.RegisterTransactionApi();
        app.RegisterLeagueLegendApi();
        app.RegisterValorantApi();
        return app;
    }
}
