using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateRequest
{
    public class UpdateRequestCommand : IRequest
    {
        public int RequestId { get; set; }
        public bool IsApproved { get; set; }
    }
}
