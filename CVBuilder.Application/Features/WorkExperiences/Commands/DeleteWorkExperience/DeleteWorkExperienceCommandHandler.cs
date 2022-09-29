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
            #region fetch work experience

            var workExperienceToDelete = await repository.GetWorkExperienceByIdAsync(request.EmployeeId, request.WorkExperienceId);

            #endregion

            if (workExperienceToDelete == null)
                throw new Exceptions.NotFoundException(nameof(WorkExperience), request.WorkExperienceId);

            await repository.DeleteAsync(workExperienceToDelete);

            return Unit.Value;
        }
    }
}
