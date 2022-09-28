using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Employees.Commands.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository repository;
        private readonly IMapper mapper;

        public AddEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<AddEmployeeCommandResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming request
            var validator = new AddEmployeeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            // Mapping request to employee entity
            var employee = mapper.Map<Employee>(request);

            // Hashing Password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            employee.Password = hashedPassword;

            employee = await repository.AddEmployeeAsync(employee);

            var response = mapper.Map<AddEmployeeCommandResponse>(employee);

            return response;
        }
    }
}
