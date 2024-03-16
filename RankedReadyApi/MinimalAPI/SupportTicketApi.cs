using RankedReadyApi.Attributes;
using RankedReadyApi.Common.Models.SupportTicket;
using RankedReadyApi.Handlers;

namespace RankedReadyApi.MinimalAPI;

public static class SupportTicketApi
{
    private static string ENDPOINT_V1 = "api/v1/supportticket";

    public static void RegisterSupportTicketApi(this WebApplication app)
    {
        app.MapGet($"{ENDPOINT_V1}", SupportTicketHandler.GetAllTickets)
            .RequireAuthorization();

        app.MapPost($"{ENDPOINT_V1}/create", SupportTicketHandler.CreateTicket)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<SupportTicketModel>>();

        app.MapPut($"{ENDPOINT_V1}/change/state", SupportTicketHandler.ChangeStateTicket)
            .RequireAuthorization();

        app.MapPost($"{ENDPOINT_V1}/ask", SupportTicketHandler.AskToTicket)
            .RequireAuthorization()
            .AddEndpointFilter<ValidationFilterAttribute<AskModel>>();
    }
}
