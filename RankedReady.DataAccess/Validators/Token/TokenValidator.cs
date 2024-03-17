using FluentValidation;
using RankedReadyApi.Common.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankedReady.DataAccess.Validators.Token;

public static class TokenValidator
{
    public class TokenRefreshModelValidator : AbstractValidator<TokenRefreshModel>
    {
        public TokenRefreshModelValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().NotNull().MinimumLength(30).WithMessage("Check your Access Tokin, but its not valid");
        }
    }
}
