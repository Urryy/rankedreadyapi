namespace RankedReadyApi.Common.DataTransferObjects.SupportTicket;

public class SupportTicketDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public bool IsAnswered { get; set; }
    public string Topic { get; set; }
    public DateTime DateCreated { get; set; }
}
