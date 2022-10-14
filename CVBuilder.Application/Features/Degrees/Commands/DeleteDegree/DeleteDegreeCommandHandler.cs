using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.Degrees.Commands.DeleteDegree
{
    public class DeleteDegreeCommandHandler : IRequestHandler<DeleteDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly ILogger<DeleteDegreeCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public DeleteDegreeCommandHandler(IDegreeRepository repository, ILogger<DeleteDegreeCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(DeleteDegreeCommand request, CancellationToken cancellationToken)
        {
            // fetch degree  

            var degreeToDelete = await GetDegreeToDelete(request.EmployeeId, request.DegreeId);

            
            if (degreeToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);


            if (request.SoftDelete)
            {
                degreeToDelete.IsDeleted = true;

                await repository.UpdateAsync(degreeToDelete);

                logger.LogInformation($"Degree With Id: {request.DegreeId} Soft Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

                return Unit.Value;
            }


            await repository.DeleteAsync(degreeToDelete);

            logger.LogInformation($"Degree With Id: {request.DegreeId} Hard Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<Degree?> GetDegreeToDelete(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
