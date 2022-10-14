using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
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
            var workExperienceDetails = await GetWorkExperienceDetails(request.EmployeeId, request.WorkExperienceId);

            if (workExperienceDetails == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);


            mapper.Map(request, workExperienceDetails);
            
            await repository.UpdateAsync(workExperienceDetails);

            logger.LogInformation($"Work Experience With Id: {request.WorkExperienceId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<WorkExperience?> GetWorkExperienceDetails(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
