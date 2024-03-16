namespace RankedReadyApi.Common.Models.User;

public class AuthResponseModel
{
    public string Username { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
