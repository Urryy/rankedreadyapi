using AutoMapper;
using Microsoft.Extensions.Configuration;
using RankedReady.DataAccess.Helpers;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.User;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Enums;
using RankedReadyApi.Common.Models.User;
using RankedReadyApi.DataAccess.Extensions;

namespace RankedReadyApi.Business.Service.Implementations;

public class UserService : GenericServiceAsync<User, UserDto>, IUserService
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _srvcToken;
    public UserService(IMapper mapper, IUnitOfWork unitOfWork,
        ITokenService srvcToken, IConfiguration configuration)
        : base(mapper, unitOfWork)
    {
        _srvcToken = srvcToken;
        _configuration = configuration;
    }

    public async Task BannedUser(Guid id)
    {
        var userDto = await GetAsync(id);

        userDto.State = StateUser.Banned.ToString();
        await UpdateAsync(userDto);
    }

    public async Task ChangeEmail(string id, string newEmail)
    {
        var userDto = await GetAsync(Guid.Parse(id));
        userDto.Email = newEmail;
        await UpdateAsync(userDto);
    }

    public async Task ChangePassword(string email, string newPwd)
    {
        var userDto = await GetByExpressionAsync(i => i.Email == email);
        userDto.Password = HashPasswordHelper.HashPassword(newPwd);
        await UpdateAsync(userDto);
    }

    public async Task DeleteUser(Guid id)
    {
        await DeleteAsync(id);
    }

    public async Task<UserWithoutCredDto> GetUserById(Guid id)
    {
        var userDto = await GetAsync(id);
        return mapper.Map<UserDto, UserWithoutCredDto>(userDto);
    }

    public async Task<IEnumerable<UserWithoutCredDto>> GetUsers()
    {
        var usersDto = await GetAllAsync();
        return usersDto.Select(mapper.Map<UserDto, UserWithoutCredDto>);
    }

    public async Task<List<PeriodUsersModel>> GetUsersByPeriod(DateTime period)
    {
        var users = await GetAllAsync();
        var usersByPeriod = users.Where(i => i.DateAuthorized > period);
        var dictUsers = new Dictionary<string, List<UserDto>>();

        if (usersByPeriod.Count() == 0)
            return new List<PeriodUsersModel>();

        var groups = usersByPeriod.GroupBy(p => p.DateAuthorized).Select(d => new
        {
            dt = d.Key,
            count = d.Count(),
            persons = d.Select(t => t)
        }).ToList();

        var periodUsers = new List<PeriodUsersModel>();
        foreach (var group in groups)
        {
            periodUsers.Add(new PeriodUsersModel { Date = group.dt.ToString("d"), Count = group.count.ToString(), Users = group.persons.ToList() });
        }

        return periodUsers;
    }

    public async Task<AuthResponseModel> Login(LoginUserModel model)
    {
        var user = await GetByExpressionAsync(i => i.Email == model.Email);
        if (user.Password != HashPasswordHelper.HashPassword(model.Password))
        {
            throw new UnauthorizedAccessException("User doesn't exist");
        }

        var userDto = mapper.Map<UserDto, UserWithoutCredDto>(user);
        var authResponse = new AuthResponseModel();

        authResponse.Token = await _srvcToken.CreateToken(userDto);
        authResponse.RefreshToken = _configuration.GenerateRefreshToken();
        authResponse.Email = user.Email;
        authResponse.Role = user.Role;

        return authResponse;
    }

    public async Task<AuthResponseModel> Register(RegisterUserModel model)
    {
        var users = await GetAllAsync();
        var isExistUser = users.FirstOrDefault(i => i.Email == model.Email);
        if (isExistUser != null)
        {
            throw new Exception("User have already exist");
        }

        if (HashPasswordHelper.HashPassword(model.Password) != HashPasswordHelper.HashPassword(model.RepeatPassword))
        {
            throw new Exception("Mismatch in passwords");
        }

        var user = new User(model.Email, HashPasswordHelper.HashPassword(model.Password), Role.User);
        await AddAsync(user);

        var userDto = mapper.Map<UserDto, UserWithoutCredDto>(await GetAsync(user.Id));

        var authResponse = new AuthResponseModel();
        authResponse.Token = await _srvcToken.CreateToken(userDto);
        authResponse.RefreshToken = _configuration.GenerateRefreshToken();
        authResponse.Email = userDto.Email;
        authResponse.Role = userDto.Role;

        return authResponse;
    }

    public async Task<UserDto> RegisterByTransaction(RegisterUserModel model)
    {
        var users = await GetAllAsync();
        var isExistUser = users.FirstOrDefault(i => i.Email == model.Email);
        if (isExistUser != null)
        {
            throw new Exception("User have already exist");
        }

        if (HashPasswordHelper.HashPassword(model.Password) != HashPasswordHelper.HashPassword(model.RepeatPassword))
        {
            throw new Exception("Mismatch in passwords");
        }

        var user = new User(model.Email, model.Password, Role.User);
        await AddAsync(user);

        return await GetAsync(user.Id);
    }

    public async Task UnbannedUser(Guid id)
    {
        var userDto = await GetAsync(id);
        userDto.State = StateUser.Active.ToString();
        await UpdateAsync(userDto);
    }
}
