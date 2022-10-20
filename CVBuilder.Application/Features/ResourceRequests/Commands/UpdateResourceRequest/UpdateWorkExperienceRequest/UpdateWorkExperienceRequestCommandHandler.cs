using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateWorkExperienceRequest
{
    public class UpdateWorkExperienceRequestCommandHandler : IRequestHandler<UpdateWorkExperienceRequestCommand, UpdateWorkExperienceRequestCommandResponse>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IWorkExperienceRepository workExperienceRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public UpdateWorkExperienceRequestCommandHandler(IResourceRequestRepository resourceRequestRepository, IWorkExperienceRepository workExperienceRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.workExperienceRepository = workExperienceRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }

        public async Task<UpdateWorkExperienceRequestCommandResponse> Handle(UpdateWorkExperienceRequestCommand request, CancellationToken cancellationToken)
        {
            // check if work experience exists
            bool workExperienceExists = await GetDegreeExists(request.WorkExperienceId);

            // create new reosource request 
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var workExperienceUpdateRequest = mapper.Map<WorkExperienceUpdateRequest>(request);

            resourceRequest.WorkExperienceUpdateRequest = workExperienceUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<UpdateWorkExperienceRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(UpdateWorkExperienceRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Modify.ToString(),
                ResourceType = ResourceType.WorkExperience.ToString(),
                Reason = request.Reason
            };
        }

        private async Task<bool> GetDegreeExists(int workExperienceId)
        {
            return await workExperienceRepository.ExistsAsync(applicationUser.GetUserId(), workExperienceId);
        }
    }
}
