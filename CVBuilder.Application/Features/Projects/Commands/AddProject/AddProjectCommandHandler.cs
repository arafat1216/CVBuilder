using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.AddProject
{
    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, AddProjectCommandResponse>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public AddProjectCommandHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }
        public async Task<AddProjectCommandResponse> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            #region check if employee exists

            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            #endregion

            
            #region mapping incoming request to project entity

            var projectToCreate = mapper.Map<Project>(request);

            #endregion

            var response = await projectRepository.AddAsync(projectToCreate);

            return mapper.Map<AddProjectCommandResponse>(response);
        }
    }
}
