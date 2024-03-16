namespace RankedReadyApi.Common.DataTransferObjects.User;

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string State { get; set; }
    public string Password { get; set; }
    public DateTime DateAuthorized { get; set; }
}
