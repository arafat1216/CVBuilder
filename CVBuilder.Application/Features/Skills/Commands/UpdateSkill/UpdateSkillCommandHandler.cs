using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.UpdateSkill
{
    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
    {
        private readonly ISkillRepository skillRepository;
        private readonly IMapper mapper;

        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            this.skillRepository = skillRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            #region check if skill exists
            
            var skillExists = await skillRepository.ExistsAsync(request.EmployeeId,request.SkillId);

            if (!skillExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            #endregion

            #region validate incoming request
            
            var validator = new UpdateSkillCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            #endregion

            #region mapping incoming request to skill entity

            var skillToUpdate = mapper.Map<Skill>(request);

            #endregion

            await skillRepository.UpdateAsync(skillToUpdate);

            return Unit.Value;
        }
    }
}
