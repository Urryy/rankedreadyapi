namespace RankedReadyApi.Common.Entities;

public class TransactionStripe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public float Amount { get; set; }
    public string SessionId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string Email { get; set; }
    public TransactionStripe(Guid userId, Guid accountId, float amount, string sessionId, bool isCompleted, string email)
    {
        UserId = userId;
        AccountId = accountId;
        Amount = amount;
        SessionId = sessionId;
        IsCompleted = isCompleted;
        Email = email;
    }
}
