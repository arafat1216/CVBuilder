using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience
{
    public class UpdateWorkExperienceCommandHandler : IRequestHandler<UpdateWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;

        public UpdateWorkExperienceCommandHandler(IWorkExperienceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            #region check if work experience exists

            var workExperienceExists = await repository.ExistsAsync(request.EmployeeId, request.WorkExperienceId);

            if (!workExperienceExists)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);

            #endregion

            
            #region mapping incoming request to work experience entity

            var workExperienceToUpdate = mapper.Map<WorkExperience>(request);

            #endregion

            await repository.UpdateAsync(workExperienceToUpdate);

            return Unit.Value;
        }
    }
}
