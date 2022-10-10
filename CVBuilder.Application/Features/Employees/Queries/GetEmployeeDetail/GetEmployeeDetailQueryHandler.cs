using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailsDto>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public GetEmployeeDetailQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
        {
            
            // Get Employee Details
            var employeeDetails = await GetEmployeeDetails(request.Id);


            // Checks If Employee Exists
            if (employeeDetails == null)
            {
                throw new Exceptions.NotFoundException(nameof(Employee), request.Id);
            }

            var employeeDetailsDto = mapper.Map<EmployeeDetailsDto>(employeeDetails);

            return employeeDetailsDto;
        }

        private async Task<Employee?> GetEmployeeDetails(Guid employeeId)
        {
            return await repository.GetEmployeeByIdAsync(employeeId);
        }
    }
}
