using FluentValidation;

namespace CVBuilder.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(p => p.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ProjectId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
