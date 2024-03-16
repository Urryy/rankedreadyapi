using RankedReadyApi.Common.DataTransferObjects.User;

namespace RankedReadyApi.Common.Models.User;

public class PeriodUsersModel
{
    public string Date { get; set; } = default!;
    public string Count { get; set; } = default!;
    public List<UserDto> Users { get; set; } = new List<UserDto>();
}
