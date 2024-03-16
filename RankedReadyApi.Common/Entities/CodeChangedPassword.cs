namespace RankedReadyApi.Common.Entities;

public class CodeChangedPassword
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public CodeChangedPassword(string code, string email)
    {
        Code = code;
        Email = email;
    }
}
