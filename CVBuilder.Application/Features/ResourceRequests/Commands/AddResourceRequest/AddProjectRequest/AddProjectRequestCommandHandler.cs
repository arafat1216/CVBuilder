using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddProjectRequest
{
    public class AddProjectRequestCommandHandler : IRequestHandler<AddProjectRequestCommand, AddProjectRequestCommandResponse>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public AddProjectRequestCommandHandler(IResourceRequestRepository repository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.repository = repository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }
        public async Task<AddProjectRequestCommandResponse> Handle(AddProjectRequestCommand request, CancellationToken cancellationToken)
        {
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var projectUpdateRequest = mapper.Map<ProjectUpdateRequest>(request);

            resourceRequest.ProjectUpdateRequest = projectUpdateRequest;

            var response = await repository.AddAsync(resourceRequest);

            return mapper.Map<AddProjectRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(AddProjectRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Add.ToString(),
                ResourceType = ResourceType.Project.ToString(),
                Reason = request.Reason
            };
        }
    }
}
