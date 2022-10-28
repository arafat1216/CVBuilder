using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreesList
{
    public class GetDegreesListQueryHandler : IRequestHandler<GetDegreesListQuery, List<DegreesListDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly IMapper mapper;

        public GetDegreesListQueryHandler(IEmployeeRepository employeeRepository, IDegreeRepository degreeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.degreeRepository = degreeRepository;
            this.mapper = mapper;
        }
        public async Task<List<DegreesListDto>> Handle(GetDegreesListQuery request, CancellationToken cancellationToken)
        {
            // check if employee exits

            var employeeExists = await EmployeeExists(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);


            // fetch degrees list

            var degrees = await degreeRepository.ListAllAsync(e => e.EmployeeId == request.EmployeeId && !e.IsDeleted);


            // mapping degree entity to degree view model

            var  degreesDto = mapper.Map<List<DegreesListDto>>(degrees);


            return degreesDto;

        }

        private async Task<bool> EmployeeExists(Guid employeeId)
        {
            return await employeeRepository.EmployeeExistsAsync(employeeId);
        }
    }
}
