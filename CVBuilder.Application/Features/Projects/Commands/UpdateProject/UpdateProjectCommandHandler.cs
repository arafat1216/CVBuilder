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
            #region check if project exists

            var projectExists = await repository.ExistsAsync(request.EmployeeId, request.ProjectId);

            if (!projectExists)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);

            #endregion

            #region validate incoming request

            var validator = new UpdateProjectCommandValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            #endregion

            #region mapping incoming request to project entity

            var projectToUpdate = mapper.Map<Project>(request);

            #endregion

            await repository.UpdateAsync(projectToUpdate);

            return Unit.Value;
        }
    }
}
