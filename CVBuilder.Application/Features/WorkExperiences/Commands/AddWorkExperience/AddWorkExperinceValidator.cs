using FluentValidation;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience
{
    public class AddWorkExperinceValidator : AbstractValidator<AddWorkExperienceCommand>
    {
        public AddWorkExperinceValidator()
        {
            RuleFor(w => w.Company).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(w => w.Designation).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();

            RuleFor(w => w.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
