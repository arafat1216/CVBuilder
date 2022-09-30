using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQueryHandler : IRequestHandler<GetProjectsListQuery, List<ProjectViewModel>>
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
        public async Task<List<ProjectViewModel>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            #region check if employee exists

            var employeeExits = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExits)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            #endregion

            #region fetch projects list

            var projectsList = await projectRepository.ListAllAsync(e => e.EmployeeId == request.EmployeeId);

            #endregion

            #region mapping projects list to project view model

            var projectsListDto = mapper.Map<List<ProjectViewModel>>(projectsList);

            #endregion

            return projectsListDto; 
        }
    }
}
