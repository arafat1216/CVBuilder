using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public bool SoftDelete { get; set; }
    }
}
