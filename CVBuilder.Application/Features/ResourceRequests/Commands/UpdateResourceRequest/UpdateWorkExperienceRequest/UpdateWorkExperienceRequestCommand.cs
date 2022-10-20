using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateWorkExperienceRequest
{
    public class UpdateWorkExperienceRequestCommand : IRequest<UpdateWorkExperienceRequestCommandResponse>
    {
        [Required]
        public int WorkExperienceId { get; set; }
        
        public string? Designation { get; set; }
        
        public string? Company { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        [Required]
        public string Reason { get; set; }

    }
}
