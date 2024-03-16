using Microsoft.AspNetCore.Mvc;
using RankedReadyApi.Attributes;
using RankedReadyApi.Business.Accessors;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.SupportTicket;

namespace RankedReadyApi.Handlers;

public static class SupportTicketHandler
{
    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> GetAllTickets(ISupportTicketService srvcSupportTicket)
    {
        var tickets = await srvcSupportTicket.GetAllTickets();
        return Results.Json(tickets);
    }

    public static async Task<IResult> CreateTicket([FromBody] SupportTicketModel model,
        ICurrentUserAccessor accessor,
        ISupportTicketService srvcSupportTicket)
    {
        var userId = accessor.GetCurrentUserId();
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
        {
            return Results.Unauthorized();
        }

        await srvcSupportTicket.CreateSupportTicket(model, id);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> ChangeStateTicket([FromQuery] bool isAnswered,
        [FromQuery] Guid objectId,
        ISupportTicketService srvcSupportTicket)
    {
        await srvcSupportTicket.SetAnsweredInTicket(objectId, isAnswered);
        return Results.Ok();
    }

    [Permission(Role.Admin, Role.Manager, Role.SuperManager)]
    public static async Task<IResult> AskToTicket([FromBody] AskModel model,
        ISupportTicketService srvcSupportTicket)
    {
        await srvcSupportTicket.AskTicket(model.TicketId, model.Text);
        return Results.Ok();
    }
}
