namespace RankedReadyApi.Common.DataTransferObjects.User;

public class UserWithoutCredDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string State { get; set; }
    public DateTime DateAuthorized { get; set; }
}
