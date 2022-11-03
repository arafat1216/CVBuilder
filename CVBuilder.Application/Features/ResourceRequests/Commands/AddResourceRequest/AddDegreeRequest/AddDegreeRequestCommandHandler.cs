using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddDegreeRequest
{
    public class AddDegreeRequestCommandHandler : IRequestHandler<AddDegreeRequestCommand, AddDegreeRequestCommandResponse>
    {
        private readonly IResourceRequestRepository repository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public AddDegreeRequestCommandHandler(IResourceRequestRepository repository, IApplicationUser applicationUser,
            IMapper mapper)
        {
            this.repository = repository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;

        }
        public async Task<AddDegreeRequestCommandResponse> Handle(AddDegreeRequestCommand request, CancellationToken cancellationToken)
        {
            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var degreeUpdateRequest = mapper.Map<DegreeUpdateRequest>(request);

            resourceRequest.DegreeUpdateRequest = degreeUpdateRequest;

            var response = await repository.AddAsync(resourceRequest);

            return mapper.Map<AddDegreeRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(AddDegreeRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Add.ToString(),
                ResourceType = ResourceType.Degree.ToString(),
                Reason = request.Reason
            };
        }
    }
}
