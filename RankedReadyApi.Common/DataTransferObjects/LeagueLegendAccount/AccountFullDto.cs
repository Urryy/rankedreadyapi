namespace RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount;

public class AccountFullDto
{
    public string Id { get; set; }
    public string? SkinId { get; set; }
    public string? UserId { get; set; }
    public string Rank { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string EmailLogin { get; set; }
    public string EmailPassword { get; set; }
    public string Region { get; set; }
    public long AmountOfBe { get; set; }
    public double Price { get; set; }
    public string StateAccount { get; set; }
    public bool IsActiveInShop { get; set; }
}
