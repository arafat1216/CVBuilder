using CVBuilder.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee
{
    public class PartialUpdateEmployeeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public string? FullName { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }
        
        [RegularExpression(@"(^(01){1}[3-9]{1}\d{8})$",ErrorMessage ="Invalid Phone Number")]
        
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public Role? Role { get; set; }
    }
}
