using FluentValidation;

namespace CVBuilder.Application.Features.Projects.Commands.AddProject
{
    public class AddProjectCommandValidator : AbstractValidator<AddProjectCommand>
    {
        public AddProjectCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(p => p.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
