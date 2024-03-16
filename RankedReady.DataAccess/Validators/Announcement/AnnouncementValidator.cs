using FluentValidation;
using RankedReadyApi.Common.Models.Announcement;

namespace RankedReady.DataAccess.Validators.Announcement;

public class AnnouncementValidator : AbstractValidator<AnnouncementModel>
{
    public AnnouncementValidator()
    {
        RuleFor(x => x.Heading).NotEmpty().NotNull().WithMessage("Field [heading] is empty");
        RuleFor(x => x.SubTitle).NotEmpty().NotNull().WithMessage("Field [subtitle] is empty");
        RuleFor(x => x.AnnouncementType).NotEmpty().NotNull().WithMessage("Field [announcementType] is empty");
    }
}
