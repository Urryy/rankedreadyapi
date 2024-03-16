namespace RankedReadyApi.Common.Models.User;

public class RegisterUserModel
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RepeatPassword { get; set; } = default!;
}
