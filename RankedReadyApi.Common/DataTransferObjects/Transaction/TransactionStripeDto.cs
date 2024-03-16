namespace RankedReadyApi.Common.DataTransferObjects.Transaction;

public class TransactionStripeDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string AccountId { get; set; }
    public float Amount { get; set; }
    public string SessionId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Email { get; set; }
}
