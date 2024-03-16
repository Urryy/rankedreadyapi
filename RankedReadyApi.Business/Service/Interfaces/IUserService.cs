using RankedReadyApi.Common.DataTransferObjects.User;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.User;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface IUserService : IGenericServiceAsync<User, UserDto>
{
    Task<IEnumerable<UserWithoutCredDto>> GetUsers();
    Task<UserWithoutCredDto> GetUserById(Guid id);
    Task DeleteUser(Guid id);
    Task ChangePassword(string email, string newPwd);
    Task ChangeEmail(string id, string newEmail);
    Task BannedUser(Guid id);
    Task UnbannedUser(Guid id);
    Task<List<PeriodUsersModel>> GetUsersByPeriod(DateTime period);

    Task<AuthResponseModel> Register(RegisterUserModel model);
    Task<AuthResponseModel> Login(LoginUserModel model);
    Task<UserDto> RegisterByTransaction(RegisterUserModel model);
}
