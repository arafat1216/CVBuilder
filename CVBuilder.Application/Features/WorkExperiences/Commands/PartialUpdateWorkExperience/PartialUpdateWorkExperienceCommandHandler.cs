using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience
{
    public class PartialUpdateWorkExperienceCommandHandler : IRequestHandler<PartialUpdateWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;

        public PartialUpdateWorkExperienceCommandHandler(IWorkExperienceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(PartialUpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperienceDetails = await GetWorkExperienceDetails(request.EmployeeId, request.WorkExperienceId);

            if (workExperienceDetails == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);


            mapper.Map(request, workExperienceDetails);
            
            await repository.UpdateAsync(workExperienceDetails);

            return Unit.Value;
        }

        private async Task<WorkExperience?> GetWorkExperienceDetails(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
