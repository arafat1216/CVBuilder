using FluentValidation;

namespace CVBuilder.Application.Features.Degrees.Commands.UpdateDegree
{
    public class UpdateDegreeCommandValidator : AbstractValidator<UpdateDegreeCommand>
    {
        public UpdateDegreeCommandValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(d => d.Institute).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(d => d.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(d => d.DegreeId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
