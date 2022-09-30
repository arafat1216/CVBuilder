using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Projects.Queries.GetProjectDetails
{
    public class GetProjectDetailsQueryHandler : IRequestHandler<GetProjectDetailsQuery, ProjectViewModel>
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;

        public GetProjectDetailsQueryHandler(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProjectViewModel> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            #region fetch project details

            var projectDetails = await repository.GetProjectByIdAsync(request.EmployeeId, request.ProjectId);

            #endregion

            #region check if project exists

            if (projectDetails == null)
                throw new Exceptions.NotFoundException(nameof(Project), request.ProjectId);

            #endregion

            return mapper.Map<ProjectViewModel>(projectDetails);
        }
    }
}
