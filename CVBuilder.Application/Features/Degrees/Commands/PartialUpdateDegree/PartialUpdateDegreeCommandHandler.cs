using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.ValueObjects;
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
            var degree = await GetDegreeDetails(request.EmployeeId, request.DegreeId);

            if (degree == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);



            request = UpdateRequest(request, degree.DegreeDetails);

            mapper.Map(request, degree);

            await repository.UpdateAsync(degree);

            logger.LogInformation($"Degree With Id: {request.DegreeId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private PartialUpdateDegreeCommand UpdateRequest(PartialUpdateDegreeCommand request, DegreeDetails degreeDetails)
        {
            request.Name = request.Name ?? degreeDetails.Name;

            request.Subject = request.Subject ?? degreeDetails.Subject;

            request.Institute = request.Institute ?? degreeDetails.Institute;

            return request;
        }

        private async Task<Degree?> GetDegreeDetails(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
