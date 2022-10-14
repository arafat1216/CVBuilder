using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Degrees.Commands.UpdateDegree
{
    public class UpdateDegreeCommandHandler : IRequestHandler<UpdateDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateDegreeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public UpdateDegreeCommandHandler(IDegreeRepository repository, IMapper mapper, ILogger<UpdateDegreeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }


        public async Task<Unit> Handle(UpdateDegreeCommand request, CancellationToken cancellationToken)
        {
            // check if degree exists

            var degreeExists = await DegreeExists(request.EmployeeId, request.DegreeId);

            if (!degreeExists)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);

            
            // mapping incoming request to degree entity

            var degreeToUpdate = mapper.Map<Degree>(request);

            
            await repository.UpdateAsync(degreeToUpdate);

            logger.LogInformation($"Degree With Id: {request.DegreeId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<bool> DegreeExists(Guid employeeId, int degreeId)
        {
            return await repository.ExistsAsync(employeeId, degreeId);
        }
    }
}
