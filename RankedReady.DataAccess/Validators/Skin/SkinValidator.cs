using FluentValidation;
using RankedReadyApi.Common.Models.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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