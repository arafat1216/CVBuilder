using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree
{
    public class PartialUpdateDegreeCommandHandler : IRequestHandler<PartialUpdateDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;

        public PartialUpdateDegreeCommandHandler(IDegreeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(PartialUpdateDegreeCommand request, CancellationToken cancellationToken)
        {
            var degreeDetails = await GetDegreeDetails(request.EmployeeId, request.DegreeId);

            if (degreeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);


            mapper.Map(request, degreeDetails);

            await repository.UpdateAsync(degreeDetails);

            return Unit.Value;
        }

        private async Task<Degree?> GetDegreeDetails(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
