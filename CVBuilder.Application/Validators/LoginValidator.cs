using CVBuilder.Application.ViewModels.Account;
using FluentValidation;

namespace CVBuilder.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("Invalid Email Address")
                .NotNull();

            RuleFor(u => u.Password).NotNull().WithMessage("{PropertyName is  required.}");
        }
    }
}
