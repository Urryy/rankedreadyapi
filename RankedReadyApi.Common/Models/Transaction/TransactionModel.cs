namespace RankedReadyApi.Common.Models.Transaction;

public class TransactionModel
{
    public Guid? UserId { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public Guid AccountId { get; set; }
    public string? callbackUrl { get; set; } = default!;
    public string? returnUrl { get; set; } = default!;
}
