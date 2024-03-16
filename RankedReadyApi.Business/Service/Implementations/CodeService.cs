using AutoMapper;
using FluentEmail.Core;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.Cosntants.EmailBody;
using RankedReadyApi.Common.DataTransferObjects.Code;
using RankedReadyApi.Common.Entities;

namespace RankedReadyApi.Business.Service.Implementations;

public class CodeService : GenericServiceAsync<CodeChangedPassword, CodeChangedPasswordDto>, ICodeService
{
    private readonly IFluentEmail _fluentEmail;
    public CodeService(IMapper mapper, IUnitOfWork unitOfWork,
            IFluentEmail fluentEmail) : base(mapper, unitOfWork)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task<bool> CheckIsActiveCode(string code)
    {
        var entityCode = await GetByExpressionAsync(i => i.Code == code);

        if (entityCode == null || entityCode.IsActive == false)
        {
            throw new NullReferenceException("This code doesn't exist or non-active");
        }

        return true;
    }

    public async Task SendCodeForChangePassword(string email)
    {
        var random = new Random();
        var randomNumber = random.Next(100000, 999999).ToString();

        var emailForSend = _fluentEmail.To(email)
            .Subject("RANKED READY - Code for change password")
            .Body(string.Format(EmailBodyConsts.EmailBodyMessageChangeCode, randomNumber));

        var sendResponse = await emailForSend.SendAsync();
        if (!sendResponse.Successful)
        {
            throw new ArgumentException("Email didn't send: " + sendResponse.ErrorMessages.FirstOrDefault());
        }

        var codeChanged = new CodeChangedPassword(randomNumber, email);
        await AddAsync(codeChanged);
    }

    public async Task SetNonActiveToCode(string code)
    {
        var entityCode = await GetByExpressionAsync(i => i.Code == code);

        if (entityCode == null || entityCode.IsActive == false)
        {
            throw new NullReferenceException("This code doesn't exist or already non-active");
        }

        entityCode.IsActive = false;
        await UpdateAsync(entityCode);
    }
}
