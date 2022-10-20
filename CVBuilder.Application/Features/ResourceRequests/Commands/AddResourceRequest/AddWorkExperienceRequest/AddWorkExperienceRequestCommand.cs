using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddWorkExperienceRequest
{
    public class AddWorkExperienceRequestCommand : IRequest<AddWorkExperienceRequestCommandResponse>
    {
        public string Designation { get; set; }

        public string Company { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Reason { get; set; }
    }
}
