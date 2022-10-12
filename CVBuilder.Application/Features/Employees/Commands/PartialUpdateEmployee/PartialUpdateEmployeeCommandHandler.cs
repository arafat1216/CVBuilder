using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee
{
    public class PartialUpdateEmployeeCommandHandler : IRequestHandler<PartialUpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public PartialUpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(PartialUpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeDetails = await GetEmployeeDetails(request.EmployeeId);

            if (employeeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);


            mapper.Map(request, employeeDetails);

            await repository.UpdateEmployeePartiallyAsync(employeeDetails);

            return Unit.Value;
        }

        private async Task<Employee?> GetEmployeeDetails(Guid employeeId)
        {
            return await repository.GetEmployeeDetailsAsync(employeeId);
        }
    }
}
