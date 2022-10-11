using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee
{
    public class PartialUpdateEmployeeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public Role? Role { get; set; }
    }
}
