using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public UpdateProjectCommandHandler(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            // check if project exists

            var projectExists = await ProjectExists(request.EmployeeId, request.ProjectId);

            if (!projectExists)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);



            // mapping incoming request to project entity

            var projectToUpdate = mapper.Map<Project>(request);

            await repository.UpdateAsync(projectToUpdate);

            return Unit.Value;
        }

        private async Task<bool> ProjectExists(Guid employeeId, int projectId)
        {
            return await repository.ExistsAsync(employeeId, projectId);
        }
    }
}
