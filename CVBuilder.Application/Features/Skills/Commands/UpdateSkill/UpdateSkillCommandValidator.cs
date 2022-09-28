using FluentValidation;

namespace CVBuilder.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("{PropertyName} is required.").NotNull();
            RuleFor(s => s.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(s => s.SkillId).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
