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

            // mapping incoming request to employee entity

            var employee = mapper.Map<Employee>(request);



            // hashing password

            employee.Password = GetHashedPassword(request.Password);
            

            employee = await repository.AddEmployeeAsync(employee);

            var response = mapper.Map<AddEmployeeCommandResponse>(employee);

            return response;
        }

        private string GetHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
