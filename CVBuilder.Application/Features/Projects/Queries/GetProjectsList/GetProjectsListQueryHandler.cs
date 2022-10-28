using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQueryHandler : IRequestHandler<GetProjectsListQuery, List<ProjectsListDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public GetProjectsListQueryHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }
        public async Task<List<ProjectsListDto>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            // check if employee exists

            var employeeExits = await EmployeeExists(request.EmployeeId);

            if (!employeeExits)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);


            // fetch projects list

            var projectsList = await projectRepository.ListAllAsync(e => e.EmployeeId == request.EmployeeId && !e.IsDeleted);

            
            // mapping projects list to project view model

            var projectsListDto = mapper.Map<List<ProjectsListDto>>(projectsList);


            return projectsListDto;
        }

        private async Task<bool> EmployeeExists(Guid employeeId)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeId);
        }
    }
}
