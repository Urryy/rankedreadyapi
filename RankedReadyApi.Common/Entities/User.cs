using RankedReadyApi.Common.Enums;

namespace RankedReadyApi.Common.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public StateUser State { get; set; } = StateUser.Active;
    public DateTime DateAuthorized { get; set; } = DateTime.UtcNow;

    public User(string email, string password, Role role)
    {
        Email = email;
        Password = password;
        Role = role;
    }
}
