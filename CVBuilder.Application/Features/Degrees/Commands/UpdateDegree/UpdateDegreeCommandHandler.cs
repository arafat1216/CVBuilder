using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.UpdateDegree
{
    public class UpdateDegreeCommandHandler : IRequestHandler<UpdateDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;

        public UpdateDegreeCommandHandler(IDegreeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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

            return Unit.Value;
        }

        private async Task<bool> DegreeExists(Guid employeeId, int degreeId)
        {
            return await repository.ExistsAsync(employeeId, degreeId);
        }
    }
}
