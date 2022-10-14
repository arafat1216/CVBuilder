using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree
{
    public class PartialUpdateDegreeCommandHandler : IRequestHandler<PartialUpdateDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<PartialUpdateDegreeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public PartialUpdateDegreeCommandHandler(IDegreeRepository repository, IMapper mapper, ILogger<PartialUpdateDegreeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(PartialUpdateDegreeCommand request, CancellationToken cancellationToken)
        {
            var degreeDetails = await GetDegreeDetails(request.EmployeeId, request.DegreeId);

            if (degreeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);


            mapper.Map(request, degreeDetails);

            await repository.UpdateAsync(degreeDetails);

            logger.LogInformation($"Degree With Id: {request.DegreeId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Degree?> GetDegreeDetails(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
