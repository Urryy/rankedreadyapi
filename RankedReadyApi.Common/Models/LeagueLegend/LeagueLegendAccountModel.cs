namespace RankedReadyApi.Common.Models.LeagueLegend;

public class LeagueLegendAccountModel
{
    public Guid? SkinId { get; set; } = default!;
    public string Rank { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string EmailLogin { get; set; } = default!;
    public string EmailPassword { get; set; } = default!;
    public string Region { get; set; } = default!;
    public long AmountOfBe { get; set; }
    public double Price { get; set; }
    public bool IsActiveInShop { get; set; }
}
