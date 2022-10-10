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
            // check if skill exists

            var skillExists = await SkillExists(request.EmployeeId, request.SkillId);

            if (!skillExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            
            // mapping incoming request to skill entity

            var skillToUpdate = mapper.Map<Skill>(request);

            
            await skillRepository.UpdateAsync(skillToUpdate);

            return Unit.Value;
        }

        private async Task<bool> SkillExists(Guid employeeId, int skillId)
        {
            return await skillRepository.ExistsAsync(employeeId, skillId);
        }
    }
}
