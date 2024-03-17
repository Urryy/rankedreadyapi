using FluentValidation;
using RankedReadyApi.Common.Models.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
