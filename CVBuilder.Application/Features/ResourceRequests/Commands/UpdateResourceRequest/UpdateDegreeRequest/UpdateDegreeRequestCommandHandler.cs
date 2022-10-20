using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateDegreeRequest
{
    public class UpdateDegreeRequestCommandHandler : IRequestHandler<UpdateDegreeRequestCommand, UpdateDegreeRequestCommandResponse>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;

        public UpdateDegreeRequestCommandHandler(IResourceRequestRepository resourceRequestRepository, IDegreeRepository degreeRepository, IApplicationUser applicationUser, IMapper mapper)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.degreeRepository = degreeRepository;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
        }
        public async Task<UpdateDegreeRequestCommandResponse> Handle(UpdateDegreeRequestCommand request, CancellationToken cancellationToken)
        {
            // check if degree exists

            var degreeExists = await GetDegreeExists(request.DegreeId);

            if (!degreeExists)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);


            // create new request

            ResourceRequest resourceRequest = CreateResourceRequest(request);

            var degreeUpdateRequest = mapper.Map<DegreeUpdateRequest>(request);

            resourceRequest.DegreeUpdateRequest = degreeUpdateRequest;

            var response = await resourceRequestRepository.AddAsync(resourceRequest);

            return mapper.Map<UpdateDegreeRequestCommandResponse>(response);
        }

        private ResourceRequest CreateResourceRequest(UpdateDegreeRequestCommand request)
        {
            return new ResourceRequest()
            {
                AppliedBy = applicationUser.GetUserId(),
                RequestType = RequestType.Modify.ToString(),
                ResourceType = ResourceType.Degree.ToString(),
                Reason = request.Reason
            };
        }

        private async Task<bool> GetDegreeExists(int degreeId)
        {
            return await degreeRepository.ExistsAsync(applicationUser.GetUserId(), degreeId);
        }
    }
}
