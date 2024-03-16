using RankedReadyApi.Common.DataTransferObjects.Code;
using RankedReadyApi.Common.Entities;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ICodeService : IGenericServiceAsync<CodeChangedPassword, CodeChangedPasswordDto>
{
    Task SendCodeForChangePassword(string email);
    Task<bool> CheckIsActiveCode(string code);
    Task SetNonActiveToCode(string code);
}

