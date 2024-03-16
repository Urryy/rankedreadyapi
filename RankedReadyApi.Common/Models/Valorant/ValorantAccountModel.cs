namespace RankedReadyApi.Common.Models.Valorant;

public class ValorantAccountModel
{
    public Guid? SkinId { get; set; } = default!;
    public string Rank { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string EmailLogin { get; set; } = default!;
    public string EmailPassword { get; set; } = default!;
    public string Region { get; set; } = default!;
    public double Price { get; set; }
    public bool IsActiveInShop { get; set; }
}
