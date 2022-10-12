using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree
{
    public class PartialUpdateDegreeCommandHandler : IRequestHandler<PartialUpdateDegreeCommand>
    {
        private readonly IDegreeRepository repository;

        public PartialUpdateDegreeCommandHandler(IDegreeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(PartialUpdateDegreeCommand request, CancellationToken cancellationToken)
        {
            var degreeDetails = await GetDegreeDetails(request.EmployeeId, request.DegreeId);

            if (degreeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);


            degreeDetails.Name = request.Name ?? degreeDetails.Name;

            degreeDetails.Institute = request.Institute ?? degreeDetails.Institute;

            await repository.UpdateAsync(degreeDetails);

            return Unit.Value;
        }

        private async Task<Degree?> GetDegreeDetails(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
