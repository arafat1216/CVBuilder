using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddSkillRequest
{
    public class AddSkillRequestCommand : IRequest<AddSkillRequestCommandResponse>
    {
        public string Name { get; set; }
        public string Reason { get; set; }
    }
}
