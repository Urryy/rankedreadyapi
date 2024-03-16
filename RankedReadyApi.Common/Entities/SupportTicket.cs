namespace RankedReadyApi.Common.Entities;

public class SupportTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public bool IsAnswered { get; set; }
    public string Topic { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public SupportTicket(Guid userId, string email, string text, bool isAnswered, string topic)
    {
        UserId = userId;
        Email = email;
        Text = text;
        IsAnswered = isAnswered;
        Topic = topic;
    }
}
