using FluentValidation;
using RankedReadyApi.Common.Models.Valorant;

namespace RankedReady.DataAccess.Validators.Valorant;

public static class ValorantValidator
{
    public class ValorantModelValidator : AbstractValidator<ValorantAccountModel>
    {
        public ValorantModelValidator()
        {
            RuleFor(x => x.Rank).NotNull().WithMessage("Rank is null or empty");
            RuleFor(x => x.Login).NotNull().WithMessage("Login is null or empty");
            RuleFor(x => x.Password).NotNull().WithMessage("Password is null or empty");
            RuleFor(x => x.EmailLogin).NotNull().WithMessage("EmailLogin is null or empty");
            RuleFor(x => x.EmailPassword).NotNull().WithMessage("EmailPassword is null or empty");
            RuleFor(x => x.Region).NotNull().WithMessage("Region is null or empty");
            RuleFor(x => x.Price).InclusiveBetween(10, 10000000).WithMessage("Price of be must be more than 10 and less than 1000000");
        }
    }
}
