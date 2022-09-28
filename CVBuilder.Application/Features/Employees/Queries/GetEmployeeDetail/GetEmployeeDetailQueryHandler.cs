using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailViewModel>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public GetEmployeeDetailQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EmployeeDetailViewModel> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
        {
            // Checks If Employee Exists

            var employeeExists = await repository.EmployeeExistsAsync(request.Id);

            if (!employeeExists)
            {
                throw new Exceptions.NotFoundException(nameof(Employee), request.Id);
            }

            // Get Employee Details
            var employeeDetails = await repository.GetEmployeeByIdAsync(request.Id);
            var employeeDetailsDto = mapper.Map<EmployeeDetailViewModel>(employeeDetails);

            // Inlcude Skills

            // Inlcude Degress

            // Include Work Experiences

            // Include Projects

            return employeeDetailsDto;
        }
    }
}
