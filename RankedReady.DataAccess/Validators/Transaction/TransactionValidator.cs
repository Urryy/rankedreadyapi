using FluentValidation;
using RankedReadyApi.Common.Models.Transaction;

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
