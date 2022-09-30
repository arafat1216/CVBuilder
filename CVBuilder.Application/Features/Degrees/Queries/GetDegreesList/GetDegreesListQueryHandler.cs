using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Queries.GetDegreesList
{
    public class GetDegreesListQueryHandler : IRequestHandler<GetDegreesListQuery, List<DegreeViewModel>>
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
        public async Task<List<DegreeViewModel>> Handle(GetDegreesListQuery request, CancellationToken cancellationToken)
        {
            #region check if employee exits
            
            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            #endregion

            #region fetch degrees list

            var degrees = await degreeRepository.ListAllAsync(e => e.EmployeeId == request.EmployeeId);
            
            #endregion

            #region mapping degree entity to degree view model

            var degreesDto = mapper.Map<List<DegreeViewModel>>(degrees);

            #endregion

            return degreesDto;

            
        }
    }
}
