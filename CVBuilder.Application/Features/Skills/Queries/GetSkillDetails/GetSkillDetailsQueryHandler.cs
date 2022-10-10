using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillDetails
{
    public class GetSkillDetailsQueryHandler : IRequestHandler<GetSkillDetailsQuery,SkillDetailsDto>
    {
        private readonly ISkillRepository repository;
        private readonly IMapper mapper;

        public GetSkillDetailsQueryHandler(ISkillRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<SkillDetailsDto> Handle(GetSkillDetailsQuery request, CancellationToken cancellationToken)
        {

            // fetch skill details 

            var skillDetails = await GetSkillDetails(request.EmployeeId, request.SkillId);


            // check if skill exists

            if (skillDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            
            return mapper.Map<SkillDetailsDto>(skillDetails);
        }

        private async Task<Skill?> GetSkillDetails(Guid employeeId, int skillId)
        {
            return await repository.GetSkillByIdAsync(employeeId, skillId);
        }
    }
}
