using FluentValidation;
using RankedReadyApi.Common.Models.Statistic;

namespace RankedReady.DataAccess.Validators.Statistic;

public static class StatisticValidator
{
    public class PeriodSortingModelValidator : AbstractValidator<PeriodSortingModel>
    {
        public PeriodSortingModelValidator()
        {
            RuleFor(x => x.PeriodSort).NotEmpty().NotNull().WithMessage("Period sort is null or empty");
        }
    }
}
