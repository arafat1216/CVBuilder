using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddDegreeRequest
{
    public class AddDegreeRequestCommand : IRequest<AddDegreeRequestCommandResponse>
    {
        public string Name { get; set; }
        public string? Subject { get; set; }
        public string Institute { get; set; }
        public string Reason { get; set; }
    }
}
