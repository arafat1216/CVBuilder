using CVBuilder.Application.Dtos.Employee;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IRequest<List<EmployeesListDto>>
    {
    }
}
