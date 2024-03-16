namespace RankedReadyApi.Common.DataTransferObjects.Transaction;

public class TransactionDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string AccountId { get; set; }
    public string Email { get; set; }
    public bool? TriedToPay { get; set; }
    public bool? IsSucceed { get; set; }
    public string? Tran_Ref { get; set; }
    public bool? IsValid_Signature { get; set; }
    public int ProfileId { get; set; }
    public string ServerKey { get; set; }
    public string Endpoint { get; set; }
    public string TranType { get; set; }
    public string TranClass { get; set; }
    public string CartId { get; set; }
    public string CartCurrency { get; set; }
    public float CartAmount { get; set; }
    public string CartDescription { get; set; }
    public string PaypageLang { get; set; }
    public bool HideShipping { get; set; }
    public bool IsFramed { get; set; }
    public string ReturnURL { get; set; }
    public string CallbackURL { get; set; }
    public DateTime DateTransaction { get; set; }
}
