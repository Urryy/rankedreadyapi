using RankedReadyApi.Common.Enums;

namespace RankedReadyApi.Common.Entities;

public class ValorantAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? SkinId { get; set; }
    public Guid? UserId { get; set; }
    public RankValorant Rank { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string EmailLogin { get; set; }
    public string EmailPassword { get; set; }
    public string Region { get; set; }
    public double Price { get; set; }
    public StateAccount StateAccount { get; set; } = StateAccount.Active;
    public bool IsActiveInShop { get; set; }
    public Skin Skin { get; set; }
    public User User { get; set; }

    public ValorantAccount(RankValorant rank, string login, string password,
            string region, double price, bool isActiveInShop,
            string emailLogin, string emailPassword)
    {
        Rank = rank;
        Login = login;
        Password = password;
        Region = region;
        Price = price;
        IsActiveInShop = isActiveInShop;
        EmailPassword = emailPassword;
        EmailLogin = emailLogin;
    }
}
