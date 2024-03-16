using RankedReadyApi.Common.Enums;

namespace RankedReadyApi.Common.Entities;

public class LeagueLegendAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? SkinId { get; set; }
    public Guid? UserId { get; set; }
    public RankLeagueLegend Rank { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string EmailLogin { get; set; }
    public string EmailPassword { get; set; }
    public string Region { get; set; }
    public long AmountOfBe { get; set; }
    public double Price { get; set; }
    public StateAccount StateAccount { get; set; } = StateAccount.Active;
    public bool IsActiveInShop { get; set; }
    public Skin Skin { get; set; }
    public User User { get; set; }

    public LeagueLegendAccount(RankLeagueLegend rank, string login,
        string password, string region, long amountOfBe,
        double price, bool isActiveInShop, string emailLogin, string emailPassword)
    {
        Rank = rank;
        Login = login;
        Password = password;
        Region = region;
        AmountOfBe = amountOfBe;
        Price = price;
        IsActiveInShop = isActiveInShop;
        EmailLogin = emailLogin;
        EmailPassword = emailPassword;
    }
}
