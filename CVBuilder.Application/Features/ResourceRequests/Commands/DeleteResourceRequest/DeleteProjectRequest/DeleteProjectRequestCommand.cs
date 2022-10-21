using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteProjectRequest
{
    public class DeleteProjectRequestCommand : IRequest<DeleteProjectRequestCommandResponse>
    {
        public int ProjectId { get; set; }
        public string Reason { get; set; }
    }
}
