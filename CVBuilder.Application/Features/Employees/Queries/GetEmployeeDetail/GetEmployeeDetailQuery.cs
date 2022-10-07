using CVBuilder.Application.Dtos.Employee;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
