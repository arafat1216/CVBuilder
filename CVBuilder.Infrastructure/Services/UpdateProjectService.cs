using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.DeleteProject;
using CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject;
using CVBuilder.Application.Features.Projects.Commands.UpdateProject;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateProjectService : IUpdateResourceService, IUpdateProjectService
    {
        private readonly IProjectUpdateRepository repository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateProjectService(IProjectUpdateRepository repository, IMediator mediator, IMapper mapper)
        {
            this.repository = repository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task UpdateResource(ResourceRequest resourceRequest)
        {
            // get resource details
            var projectUpdateRequest = await GetProjectUpdateRequest(resourceRequest.RequestId);

            if (resourceRequest.RequestType == RequestType.Add.ToString())
            {
                await AddProject(resourceRequest, projectUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Modify.ToString())
            {
                await UpdateProject(resourceRequest, projectUpdateRequest);
            }

            else if (resourceRequest.RequestType == RequestType.Remove.ToString())
            {
                await DeleteProject(resourceRequest, projectUpdateRequest);
            }
        }

        private async Task DeleteProject(ResourceRequest resourceRequest, ProjectUpdateRequest projectUpdateRequest)
        {
            var requestDto = mapper.Map<DeleteProjectCommand>(projectUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            requestDto.SoftDelete = true;

            await mediator.Send(requestDto);
        }

        private async Task UpdateProject(ResourceRequest resourceRequest, ProjectUpdateRequest projectUpdateRequest)
        {
            var requestDto = mapper.Map<UpdateProjectCommand>(projectUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task AddProject(ResourceRequest resourceRequest, ProjectUpdateRequest projectUpdateRequest)
        {
            var requestDto = mapper.Map<AddProjectCommand>(projectUpdateRequest);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        private async Task<ProjectUpdateRequest?> GetProjectUpdateRequest(int requestId)
        {
            return await repository.GetProjectUpdateRequestByIdAsync(requestId);
        }
    }
}
