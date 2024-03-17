using FluentValidation;
using RankedReadyApi.Common.Models.SupportTicket;

namespace RankedReady.DataAccess.Validators.SupportTicket;

public static class SupportTicketValidator
{
    public class SupportTicketModelValidator : AbstractValidator<SupportTicketModel>
    {
        public SupportTicketModelValidator()
        {
            RuleFor(x => x.TitleTopic).NotNull().NotEmpty().WithMessage("Title is null or empty");
            RuleFor(x => x.BodyText).NotEmpty().NotNull().WithMessage("Message in body is null or empty");
        }
    }

    public class AskModelValidator : AbstractValidator<AskModel>
    {
        public AskModelValidator()
        {
            RuleFor(x => x.TicketId).NotNull().NotEqual(Guid.Empty).WithMessage("Ticket Id is null or empty");
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Message is null or empty");
        }
    }
}
