using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddSkillRequest
{
    public class AddSkillRequestCommandHandler : IRequestHandler<AddSkillRequestCommand, AddSkillRequestCommandResponse>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public AddSkillRequestCommandHandler(IResourceRequestRepository repository, IApplicationUser applicationUser,
            IMapper mapper)
        {
            this.repository = repository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }
        public async Task<AddSkillRequestCommandResponse> Handle(AddSkillRequestCommand request, CancellationToken cancellationToken)
        {
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var skillUpdateRequest = mapper.Map<SkillUpdateRequest>(request);

            resourceRequest.SkillUpdateRequest = skillUpdateRequest;

            var response = await repository.AddAsync(resourceRequest);

            return mapper.Map<AddSkillRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(AddSkillRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Add.ToString(),
                ResourceType = ResourceType.Skill.ToString(),
                Reason = request.Reason
            };
        }
    }
}
