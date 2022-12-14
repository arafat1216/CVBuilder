using CVBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Dtos.Employee
{
    public class EmployeesListDto
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public String Role { get; set; }

    }
}
