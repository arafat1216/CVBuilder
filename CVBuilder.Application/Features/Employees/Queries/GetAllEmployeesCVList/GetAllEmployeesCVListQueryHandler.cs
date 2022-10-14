using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Models.Pagination;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetAllEmployeesCVList
{
    public class GetAllEmployeesCVListQueryHandler : IRequestHandler<GetAllEmployeesCVListQuery, (List<EmployeeDetailsDto>, PaginationMetaData)>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public GetAllEmployeesCVListQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        

        public async Task<(List<EmployeeDetailsDto>, PaginationMetaData)> Handle(GetAllEmployeesCVListQuery request, CancellationToken cancellationToken)
        {
            var (employees, metaData) = await repository.GetAllEmployeesCVAsync(request.SearchBySkill, request.searchByDegree, request.PageNumber, request.PageSize);

            var employeesDto = mapper.Map<List<EmployeeDetailsDto>>(employees);

            return (employeesDto, metaData);
        }
    }
}
