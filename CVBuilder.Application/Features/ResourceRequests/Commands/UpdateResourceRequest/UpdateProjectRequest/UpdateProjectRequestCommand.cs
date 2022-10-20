using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateProjectRequest
{
    public class UpdateProjectRequestCommand : IRequest<UpdateProjectRequestCommandResponse>
    {
        [Required]
        public int ProjectId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Link { get; set; }
        
        [Required]
        public string Reason { get; set; }
    
    }
}
