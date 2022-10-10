using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.WorkExperience;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperienceDetails
{
    public class GetWorkExperienceDetailsQueryHandler : IRequestHandler<GetWorkExperienceDetailsQuery, WorkExperienceDetailsDto>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;

        public GetWorkExperienceDetailsQueryHandler(IWorkExperienceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<WorkExperienceDetailsDto> Handle(GetWorkExperienceDetailsQuery request, CancellationToken cancellationToken)
        {
            // fetch work experience details

            var workExperienceDetails = await GetWorkExperienceDetails(request.EmployeeId, request.WorkExperienceId);


            // check if work experience exists

            if (workExperienceDetails == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);

            
            return mapper.Map<WorkExperienceDetailsDto>(workExperienceDetails);
        }

        private async Task<WorkExperience?> GetWorkExperienceDetails(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
