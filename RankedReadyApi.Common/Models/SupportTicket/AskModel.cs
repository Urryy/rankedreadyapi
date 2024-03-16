namespace RankedReadyApi.Common.Models.SupportTicket;

public class AskModel
{
    public Guid TicketId { get; set; }
    public string Text { get; set; } = default!;
}
