using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience
{
    public class PartialUpdateWorkExperienceCommandHandler : IRequestHandler<PartialUpdateWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;

        public PartialUpdateWorkExperienceCommandHandler(IWorkExperienceRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(PartialUpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperienceDetails = await GetWorkExperienceDetails(request.EmployeeId, request.WorkExperienceId);

            if (workExperienceDetails == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);


            workExperienceDetails.Designation = request.Designation ?? workExperienceDetails.Designation;

            workExperienceDetails.Company = request.Company ?? workExperienceDetails.Company;

            workExperienceDetails.StartDate = request.StartDate ?? workExperienceDetails.StartDate;

            workExperienceDetails.EndDate = request.EndDate ?? workExperienceDetails.EndDate;

            await repository.UpdateAsync(workExperienceDetails);

            return Unit.Value;
        }

        private async Task<WorkExperience?> GetWorkExperienceDetails(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
