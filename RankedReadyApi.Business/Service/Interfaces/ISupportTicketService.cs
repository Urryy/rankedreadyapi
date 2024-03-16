using RankedReadyApi.Common.DataTransferObjects.SupportTicket;
using RankedReadyApi.Common.Entities;
using RankedReadyApi.Common.Models.SupportTicket;

namespace RankedReadyApi.Business.Service.Interfaces;

public interface ISupportTicketService : IGenericServiceAsync<SupportTicket, SupportTicketDto>
{
    Task CreateSupportTicket(SupportTicketModel model, Guid userId);
    Task<IEnumerable<SupportTicketDto>> GetAllTickets();
    Task AskTicket(Guid ticketId, string text);
    Task SetAnsweredInTicket(Guid ticketId, bool answered);
}
