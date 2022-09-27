using FluentValidation;

namespace CVBuilder.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.FullName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(e => e.PhoneNo)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .Matches(@"(^(01){1}[3-9]{1}\d{8})$")
                .WithMessage("{PropertyName} is invalid.");

            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress()
                .WithMessage("{PropertyName} is invalid.");


            RuleFor(e => e.Address)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200)
                .WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(e => e.Role)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        
        }
    }
}
