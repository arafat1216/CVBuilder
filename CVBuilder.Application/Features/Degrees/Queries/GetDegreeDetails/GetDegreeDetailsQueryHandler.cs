using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreeDetails
{
    public class GetDegreeDetailsQueryHandler : IRequestHandler<GetDegreeDetailsQuery, DegreeDetailsDto>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;

        public GetDegreeDetailsQueryHandler(IDegreeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<DegreeDetailsDto> Handle(GetDegreeDetailsQuery request, CancellationToken cancellationToken)
        {
            // fetch degree details

            var degreeDetails = await GetDegreeDetails(request.EmployeeId, request.DegreeId);

            
            // check if degree exists

            if (degreeDetails == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);

            

            // mapping degree entity to degree view model

            var degreeDetailsDto = mapper.Map<DegreeDetailsDto>(degreeDetails);

            return degreeDetailsDto;
        }

        private async Task<Degree?> GetDegreeDetails(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
