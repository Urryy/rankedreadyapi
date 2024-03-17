using FluentValidation;
using RankedReadyApi.Common.Models.Skin;

namespace RankedReady.DataAccess.Validators.Skin;

public class SkinValidator
{
    public class SkinModelValidator : AbstractValidator<SkinModel>
    {
        public SkinModelValidator()
        {
            RuleFor(x => x.ChampionName).NotNull().NotEmpty().WithMessage("Champion name is empty or null");
            RuleFor(x => x.Information).NotNull().NotEmpty().WithMessage("Information about champion is empty or null");
            RuleFor(x => x.Skin).NotNull().WithMessage("Skin is null");
        }
    }
}