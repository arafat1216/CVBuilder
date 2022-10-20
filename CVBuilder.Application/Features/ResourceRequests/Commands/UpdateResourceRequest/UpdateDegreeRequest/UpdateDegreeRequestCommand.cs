using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateDegreeRequest
{
    public class UpdateDegreeRequestCommand : IRequest<UpdateDegreeRequestCommandResponse>
    {
        [Required]
        public int DegreeId { get; set; }

        public string? Name { get; set; }
        
        public string? Subject { get; set; }
        
        public string? Institute { get; set; }
        
        [Required]
        public string Reason { get; set; }
    }
}
