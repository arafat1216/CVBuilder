using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperienceDetails
{
    public class GetWorkExperienceDetailsQueryHandler : IRequestHandler<GetWorkExperienceDetailsQuery, WorkExperienceViewModel>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;

        public GetWorkExperienceDetailsQueryHandler(IWorkExperienceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<WorkExperienceViewModel> Handle(GetWorkExperienceDetailsQuery request, CancellationToken cancellationToken)
        {
            #region check if work experience exists

            var workExperienceExists = await repository.ExistsAsync(request.EmployeeId, request.WorkExperienceId);

            if (!workExperienceExists)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);

            #endregion

            var workExperienceDetails = await repository.GetWorkExperienceByIdAsync(request.EmployeeId, request.WorkExperienceId);

            return mapper.Map<WorkExperienceViewModel>(workExperienceDetails);
        }
    }
}
