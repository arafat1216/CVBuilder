using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateRequest
{
    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMediator mediator;
        private readonly IUpdateResourceService updateResourceService;

        public UpdateRequestCommandHandler(IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser, IMediator mediator, IUpdateResourceService updateResourceService)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
            this.mediator = mediator;
            this.updateResourceService = updateResourceService;
        }
        public async Task<Unit> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var requestDetails = await resourceRequestRepository.GetResourceRequestByIdAsync(request.RequestId);

            if (requestDetails == null)
                throw new Exceptions.NotFoundException(nameof(ResourceRequest), request.RequestId);

            requestDetails.ReviewedBy = applicationUser.GetUserId();


            if (!request.IsApproved)
            {
                requestDetails.Status = Status.Rejected.ToString();

                await resourceRequestRepository.UpdateAsync(requestDetails);

                return Unit.Value;
            }

            await updateResourceService.UpdateResource(requestDetails);

            requestDetails.Status = Status.Approved.ToString();

            await resourceRequestRepository.UpdateAsync(requestDetails);

            return Unit.Value;

        }

        
    }
}
