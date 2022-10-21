using CVBuilder.Application.Dtos.ResourceRequests;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQuery : IRequest<ResourceRequestDetailsDto>
    {
        public int RequestId { get; set; }
    }
}
