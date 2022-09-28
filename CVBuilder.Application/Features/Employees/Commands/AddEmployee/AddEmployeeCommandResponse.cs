using CVBuilder.Domain.Enums;

namespace CVBuilder.Application.Features.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandResponse
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
    }
}
