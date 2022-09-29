using FluentValidation;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience
{
    public class UpdateWorkExperienceCommandValidator : AbstractValidator<UpdateWorkExperienceCommand>
    {
        public UpdateWorkExperienceCommandValidator()
        {
            RuleFor(w => w.Company).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(w => w.Designation).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(w => w.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(w => w.WorkExperienceId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
