using CVBuilder.Application.ViewModels.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
