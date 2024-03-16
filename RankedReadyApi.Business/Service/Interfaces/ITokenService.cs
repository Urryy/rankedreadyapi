using RankedReadyApi.Common.DataTransferObjects.User;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(UserWithoutCredDto user);
}
