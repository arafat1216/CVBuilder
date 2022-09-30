using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels.Employee;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, List<EmployeeListViewModel>>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public GetEmployeesListQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<EmployeeListViewModel>> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
        {
            var employees = (await repository.GetAllEmployeesAsync()).OrderBy(e => e.FullName);
            return mapper.Map<List<EmployeeListViewModel>>(employees);
        }
    }
}
