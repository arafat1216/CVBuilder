using CVBuilder.Application.Dtos.ResourceRequests;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Queries.ListAllRequests
{
    public class ListAllResourceRequestsQuery : IRequest<List<ResourceRequestsListDto>>
    {

    }
}
