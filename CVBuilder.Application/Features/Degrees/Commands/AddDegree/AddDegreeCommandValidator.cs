using FluentValidation;

namespace CVBuilder.Application.Features.Degrees.Commands.AddDegree
{
    public class AddDegreeCommandValidator : AbstractValidator<AddDegreeCommand>
    {
        public AddDegreeCommandValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(d => d.Institute).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(d => d.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
