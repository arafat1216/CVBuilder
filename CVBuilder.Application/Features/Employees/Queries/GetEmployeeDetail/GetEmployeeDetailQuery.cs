using CVBuilder.Application.ViewModels.Employee;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}
