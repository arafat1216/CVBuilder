using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Degrees.Commands.AddDegree
{
    public class AddDegreeCommandHandler : IRequestHandler<AddDegreeCommand, AddDegreeCommandResponse>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AddDegreeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public AddDegreeCommandHandler(IEmployeeRepository employeeRepository, IDegreeRepository degreeRepository, IMapper mapper, ILogger<AddDegreeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.employeeRepository = employeeRepository;
            this.degreeRepository = degreeRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<AddDegreeCommandResponse> Handle(AddDegreeCommand request, CancellationToken cancellationToken)
        {
            // check if employee exists

            bool employeeExists = await EmployeeExists(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            

            // mapping incoming request to degree entity

            var degreeToAdd = mapper.Map<Degree>(request);

            

            var response = await degreeRepository.AddAsync(degreeToAdd);

            logger.LogInformation($"Degree With Id: {response.DegreeId} Added For Employee: {response.EmployeeId} By {applicationUser.GetUserId()}");

            return mapper.Map<AddDegreeCommandResponse>(response);
        }

        private async Task<bool> EmployeeExists(Guid employeeID)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeID);
        }
    }
}
