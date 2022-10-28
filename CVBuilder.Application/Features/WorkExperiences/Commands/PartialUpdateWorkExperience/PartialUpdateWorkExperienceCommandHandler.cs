using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience
{
    public class PartialUpdateWorkExperienceCommandHandler : IRequestHandler<PartialUpdateWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<PartialUpdateWorkExperienceCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public PartialUpdateWorkExperienceCommandHandler(IWorkExperienceRepository repository, IMapper mapper, ILogger<PartialUpdateWorkExperienceCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(PartialUpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = await GetWorkExperienceDetails(request.EmployeeId, request.WorkExperienceId);

            if (workExperience == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);


            request = UpdateRequest(request, workExperience.WorkExperienceDetails);

            mapper.Map(request, workExperience);

            await repository.UpdateAsync(workExperience);

            logger.LogInformation($"Work Experience With Id: {request.WorkExperienceId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private PartialUpdateWorkExperienceCommand UpdateRequest(PartialUpdateWorkExperienceCommand request, WorkExperienceDetails workExperienceDetails)
        {
            request.Designation = request.Designation ?? workExperienceDetails.Designation;

            request.Company = request.Company ?? workExperienceDetails.Company;

            request.StartDate = request.StartDate ?? workExperienceDetails.StartDate;

            request.EndDate = request.EndDate ?? workExperienceDetails.EndDate;

            return request;

        }

        private async Task<WorkExperience?> GetWorkExperienceDetails(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
