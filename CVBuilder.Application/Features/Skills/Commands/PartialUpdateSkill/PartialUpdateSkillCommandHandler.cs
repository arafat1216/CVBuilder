using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill
{
    public class PartialUpdateSkillCommandHandler : IRequestHandler<PartialUpdateSkillCommand>
    {
        private readonly ISkillRepository repository;
        private readonly IMapper mapper;

        public PartialUpdateSkillCommandHandler(ISkillRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(PartialUpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skillDetails = await GetSkillDetails(request.EmployeeId, request.SkillId);

            if (skillDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);


            mapper.Map(request, skillDetails);

            await repository.UpdateAsync(skillDetails);

            return Unit.Value;
        }

        private async Task<Skill?> GetSkillDetails(Guid employeeId, int skillId)
        {
            return await repository.GetSkillByIdAsync(employeeId, skillId);
        }
    }
}
