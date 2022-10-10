using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectDetails
{
    public class GetProjectDetailsQueryHandler : IRequestHandler<GetProjectDetailsQuery, ProjectDetailsDto>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public GetProjectDetailsQueryHandler(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProjectDetailsDto> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            // fetch project details

            var projectDetails = await GetProjectDetails(request.EmployeeId, request.ProjectId);


            // check if project exists

            if (projectDetails == null)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);


            return mapper.Map<ProjectDetailsDto>(projectDetails);
        }

        private async Task<Project?> GetProjectDetails(Guid employeeId, int projectId)
        {
            return await repository.GetProjectByIdAsync(employeeId, projectId);
        }
    }
}
