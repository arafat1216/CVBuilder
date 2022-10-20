using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateSkillRequest
{
    public class UpdateSkillRequestCommand : IRequest<UpdateSkillRequestCommandResponse>
    {
        [Required]
        public int SkillId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
