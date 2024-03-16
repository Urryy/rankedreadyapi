namespace RankedReadyApi.Common.DataTransferObjects.ValorantAccount;

public class AccountWithoutCredDto
{
    public string Id { get; set; }
    public string? SkinId { get; set; }
    public string? UserId { get; set; }
    public string Rank { get; set; }
    public string Region { get; set; }
    public double Price { get; set; }
}
