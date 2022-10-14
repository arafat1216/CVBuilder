using AutoMapper;
using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience
{
    public class DeleteWorkExperienceCommandHandler : IRequestHandler<DeleteWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteWorkExperienceCommandHandler> logger;
        private readonly IApplicationUser applicationUser;

        public DeleteWorkExperienceCommandHandler(IWorkExperienceRepository repository, IMapper mapper, ILogger<DeleteWorkExperienceCommandHandler> logger, IApplicationUser applicationUser)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.applicationUser = applicationUser;
        }
        public async Task<Unit> Handle(DeleteWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            // fetch work experience

            var workExperienceToDelete = await GetWorkExperienceToDelete(request.EmployeeId, request.WorkExperienceId);

            
            if (workExperienceToDelete == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);


            if (request.SoftDelete)
            {
                workExperienceToDelete.IsDeleted = true;

                await repository.UpdateAsync(workExperienceToDelete);

                logger.LogInformation($"Work Experience With Id: {request.WorkExperienceId} Soft Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

                return Unit.Value;
            }


            await repository.DeleteAsync(workExperienceToDelete);

            logger.LogInformation($"Work Experience With Id: {request.WorkExperienceId} Hard Deleted For Employee: {request.EmployeeId} By {applicationUser.GetUserId()}");

            return Unit.Value;
        }

        private async Task<WorkExperience?> GetWorkExperienceToDelete(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
