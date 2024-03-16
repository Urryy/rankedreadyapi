using AutoMapper;
using FluentEmail.Core;
using RankedReady.DataAccess.Repository.Interfaces;
using RankedReadyApi.Business.Service.Interfaces;
using RankedReadyApi.Common.DataTransferObjects.SupportTicket;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.SupportTicket;

namespace RankedReadyApi.Business.Service.Implementations;

public class SupportTicketService : GenericServiceAsync<SupportTicket, SupportTicketDto>, ISupportTicketService
{
    private readonly IUserService _srvcUser;
    private readonly IFluentEmail _fluentEmail;
    public SupportTicketService(IMapper mapper, IUnitOfWork unitOfWork,
        IUserService srvcUser, IFluentEmail fluentEmail) : base(mapper, unitOfWork)
    {
        _srvcUser = srvcUser;
        _fluentEmail = fluentEmail;
    }

    public async Task AskTicket(Guid ticketId, string text)
    {
        var ticket = await GetAsync(ticketId);
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("body is empty");
        }

        var sendEmail = _fluentEmail.To(ticket.Email)
                    .Subject("RANKED READY")
                    .Body(text, true);

        var sendResponse = await sendEmail.SendAsync();
        if (!sendResponse.Successful)
        {
            throw new ArgumentException(sendResponse.ErrorMessages.FirstOrDefault());
        }
    }

    public async Task CreateSupportTicket(SupportTicketModel model, Guid userId)
    {
        var user = await _srvcUser.GetAsync(userId);
        var supportTicket = new SupportTicket(userId, user.Email, model.BodyText, false, model.TitleTopic);
        await AddAsync(supportTicket);
    }

    public async Task<IEnumerable<SupportTicketDto>> GetAllTickets()
    {
        return await GetAllAsync();
    }

    public async Task SetAnsweredInTicket(Guid ticketId, bool answered)
    {
        var ticket = await GetAsync(ticketId);
        ticket.IsAnswered = true;
        await UpdateAsync(ticket);
    }
}
