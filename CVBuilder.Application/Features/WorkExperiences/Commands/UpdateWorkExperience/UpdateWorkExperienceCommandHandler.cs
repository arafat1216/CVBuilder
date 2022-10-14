using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience
{
    public class UpdateWorkExperienceCommandHandler : IRequestHandler<UpdateWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateWorkExperienceCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public UpdateWorkExperienceCommandHandler(IWorkExperienceRepository repository, IMapper mapper, ILogger<UpdateWorkExperienceCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            // check if work experience exists

            var workExperienceExists = await WorkExperienceExists(request.EmployeeId, request.WorkExperienceId);

            if (!workExperienceExists)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);


            // mapping incoming request to work experience entity

            var workExperienceToUpdate = mapper.Map<WorkExperience>(request);
            
            await repository.UpdateAsync(workExperienceToUpdate);

            logger.LogInformation($"Work Experience With Id: {request.WorkExperienceId} Updated For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<bool> WorkExperienceExists(Guid employeeId, int workExperienceId)
        {
            return await repository.ExistsAsync(employeeId, workExperienceId);
        }
    }
}
