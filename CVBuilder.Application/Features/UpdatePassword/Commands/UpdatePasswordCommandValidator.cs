using FluentValidation;

namespace CVBuilder.Application.Features.UpdatePassword.Commands
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(u => u.NewPassword).MinimumLength(8).WithMessage("{PropertyName} must be 8 characters long.");

            RuleFor(u => u.ConfirmPassword).Equal(u => u.NewPassword).WithMessage("Confirm Password & New Password must match");
        }
    }
}
