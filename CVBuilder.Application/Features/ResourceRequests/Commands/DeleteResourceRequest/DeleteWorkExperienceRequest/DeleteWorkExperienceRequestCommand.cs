using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteWorkExperienceRequest
{
    public class DeleteWorkExperienceRequestCommand : IRequest<DeleteWorkExperienceRequestCommandResponse>
    {
        public int WorkExperienceId { get; set; }
        public string Reason { get; set; }
    }
}
