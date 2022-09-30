using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillsList
{
    public class GetSkillsListQueryHandler : IRequestHandler<GetSkillsListQuery, List<SkillViewModel>>
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
        public async Task<List<SkillViewModel>> Handle(GetSkillsListQuery request, CancellationToken cancellationToken)
        {
            // check if user exits 
            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if(!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            // fetch skills list
            var skills = await skillRepository.ListAllAsync(request.EmployeeId);
            
            // mapping skill entity to skill view model
            return mapper.Map<List<SkillViewModel>>(skills);

        }
    }
}
