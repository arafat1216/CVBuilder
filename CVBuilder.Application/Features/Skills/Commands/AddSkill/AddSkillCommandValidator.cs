using CVBuilder.Domain.Entities;
using FluentValidation;

namespace CVBuilder.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommandValidator : AbstractValidator<AddSkillCommand>
    {
        public AddSkillCommandValidator()
        {
            RuleFor(s => s.Name).NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(s => s.EmployeeId).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
