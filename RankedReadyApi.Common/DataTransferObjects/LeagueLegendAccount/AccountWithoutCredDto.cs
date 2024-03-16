namespace RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount;

public class AccountWithoutCredDto
{
    public string Id { get; set; }
    public string? SkinId { get; set; }
    public string? UserId { get; set; }
    public string Rank { get; set; }
    public string Region { get; set; }
    public long AmountOfBe { get; set; }
    public double Price { get; set; }
}
