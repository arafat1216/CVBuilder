using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddWorkExperienceRequest
{
    public class AddWorkExperienceRequestCommandHandler : IRequestHandler<AddWorkExperienceRequestCommand, AddWorkExperienceRequestCommandResponse>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public AddWorkExperienceRequestCommandHandler(IResourceRequestRepository repository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.repository = repository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }
        public async Task<AddWorkExperienceRequestCommandResponse> Handle(AddWorkExperienceRequestCommand request, CancellationToken cancellationToken)
        {
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var workExperienceUpdateRequest = mapper.Map<WorkExperienceUpdateRequest>(request);

            resourceRequest.WorkExperienceUpdateRequest = workExperienceUpdateRequest;

            var response = await repository.AddAsync(resourceRequest);

            return mapper.Map<AddWorkExperienceRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(AddWorkExperienceRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Add.ToString(),
                ResourceType = ResourceType.WorkExperience.ToString(),
                Reason = request.Reason
            };
        }
    }
}
