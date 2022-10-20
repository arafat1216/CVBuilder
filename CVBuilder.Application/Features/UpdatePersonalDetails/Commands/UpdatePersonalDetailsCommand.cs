using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.Features.UpdatePersonalDetails.Commands
{
    public class UpdatePersonalDetailsCommand : IRequest<UpdatePersonalDetailsCommandResponse>
    {
        public Guid EmployeeId { get; set; }
        
        [Required]
        public string Reason { get; set; }

        public string? FullName { get; set; }

        [RegularExpression(@"(^(01){1}[3-9]{1}\d{8})$", ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNo { get; set; }

        public string? Address { get; set; }
        
    }
}
