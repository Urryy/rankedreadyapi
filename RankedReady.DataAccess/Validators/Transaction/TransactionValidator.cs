using FluentValidation;
using RankedReadyApi.Common.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankedReady.DataAccess.Validators.Transaction;

public static class TransactionValidator
{
    public class TransactionModelValidator : AbstractValidator<TransactionModel>
    {
        public TransactionModelValidator()
        {
            RuleFor(x => x.AccountId).NotNull().NotEqual(Guid.Empty).WithMessage("Account Id is null or empty");
        }
    }
}
