using FluentValidation;
using RankedReadyApi.Common.Models.User;

namespace RankedReady.DataAccess.Validators.User;

public static class UserValidator
{
    public class LoginUserValidator : AbstractValidator<LoginUserModel>
    {
        public LoginUserValidator()
        {
            RuleFor(i => i.Email).NotEmpty().WithMessage("Email is empty");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password is empty");
        }
    }

    public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("password is empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("email is empty");
            RuleFor(x => x.RepeatPassword).NotEmpty().WithMessage("repeat password is empty");
        }
    }


}
