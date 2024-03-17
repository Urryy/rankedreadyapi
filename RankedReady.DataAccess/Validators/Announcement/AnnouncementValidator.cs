using FluentValidation;
using RankedReadyApi.Common.Models.Announcement;

namespace RankedReady.DataAccess.Validators.Announcement;

public static class AnnouncementValidator
{
    public class AnnouncementModelValidator : AbstractValidator<AnnouncementModel>
    {
        public AnnouncementModelValidator()
        {
            RuleFor(x => x.Heading).NotEmpty().NotNull().WithMessage("Field [heading] is empty");
            RuleFor(x => x.SubTitle).NotEmpty().NotNull().WithMessage("Field [subtitle] is empty");
            RuleFor(x => x.AnnouncementType).NotEmpty().NotNull().WithMessage("Field [announcementType] is empty");
        }
    }
}
