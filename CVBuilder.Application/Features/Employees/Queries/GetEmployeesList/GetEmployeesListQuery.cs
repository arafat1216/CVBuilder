using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Models.Pagination;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQuery : IRequest<(List<EmployeesListDto>, PaginationMetaData)>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
