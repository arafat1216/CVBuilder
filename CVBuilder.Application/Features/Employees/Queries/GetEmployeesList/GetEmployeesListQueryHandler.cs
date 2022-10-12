using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Models.Pagination;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeesList
{
    public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, (List<EmployeesListDto>, PaginationMetaData)>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public GetEmployeesListQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<(List<EmployeesListDto>, PaginationMetaData)> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
        {
            var (employees, metaData) = await repository.GetAllEmployeesAsync(request.PageNumber, request.PageSize);
            
            var employeesDto =  mapper.Map<List<EmployeesListDto>>(employees);

            return (employeesDto, metaData);
        }
    }
}
