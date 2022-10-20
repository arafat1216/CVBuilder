using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddProjectRequest
{
    public class AddProjectRequestCommand : IRequest<AddProjectRequestCommandResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string Reason { get; set; }
    }
}
