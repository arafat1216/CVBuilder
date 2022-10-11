using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience
{
    public class DeleteWorkExperienceCommandHandler : IRequestHandler<DeleteWorkExperienceCommand>
    {
        private readonly IWorkExperienceRepository repository;
        private readonly IMapper mapper;

        public DeleteWorkExperienceCommandHandler(IWorkExperienceRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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

                return Unit.Value;
            }


            await repository.DeleteAsync(workExperienceToDelete);

            return Unit.Value;
        }

        private async Task<WorkExperience?> GetWorkExperienceToDelete(Guid employeeId, int workExperienceId)
        {
            return await repository.GetWorkExperienceByIdAsync(employeeId, workExperienceId);
        }
    }
}
