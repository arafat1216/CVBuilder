using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteSkillRequest
{
    public class DeleteSkillRequestCommand : IRequest<DeleteSkillRequestCommandResponse>
    {
        public int SkillId { get; set; }
        public string Reason { get; set; }
    }
}
