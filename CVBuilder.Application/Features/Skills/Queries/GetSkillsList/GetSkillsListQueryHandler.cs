using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillsList
{
    public class GetSkillsListQueryHandler : IRequestHandler<GetSkillsListQuery, List<SkillDetailsDto>>
    {
        private readonly ISkillRepository skillRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public GetSkillsListQueryHandler(ISkillRepository skillRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.skillRepository = skillRepository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        public async Task<List<SkillDetailsDto>> Handle(GetSkillsListQuery request, CancellationToken cancellationToken)
        {
            // check if user exists

            var employeeExists = await EmployeeExists(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);



            // fetch skills list

            var skills = await skillRepository.ListAllAsync(e => e.EmployeeId == request.EmployeeId && !e.IsDeleted);

            
            // mapping skill entity to skill view model

            return mapper.Map<List<SkillDetailsDto>>(skills);

        }

        private async Task<bool> EmployeeExists(Guid employeeId)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeId);
        }
    }
}
