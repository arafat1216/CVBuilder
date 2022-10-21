using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteDegreeRequest
{
    public class DeleteDegreeRequestCommand : IRequest<DeleteDegreeRequestCommandResponse>
    {
        public int DegreeId { get; set; }
        public string Reason { get; set; }
    }
}
