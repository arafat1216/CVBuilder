using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject
{
    public class PartialUpdateProjectCommandHandler : IRequestHandler<PartialUpdateProjectCommand>
    {
        private readonly IProjectRepository repository;

        public PartialUpdateProjectCommandHandler(IProjectRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(PartialUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var projectDetails = await GetProjectDetails(request.EmployeeId, request.ProjectId);

            if (projectDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.ProjectId);


            projectDetails.Name = request.Name ?? projectDetails.Name;

            projectDetails.Description = request.Description ?? projectDetails.Description;

            projectDetails.Link = request.Link ?? projectDetails.Link;

            await repository.UpdateAsync(projectDetails);

            return Unit.Value;
        }

        private async Task<Project?> GetProjectDetails(Guid employeeId, int projectId)
        {
            return await repository.GetProjectByIdAsync(employeeId, projectId);
        }
    }
}
