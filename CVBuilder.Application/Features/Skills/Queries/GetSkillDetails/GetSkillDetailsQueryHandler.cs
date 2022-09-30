using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Skills.Queries.GetSkillDetails
{
    public class GetSkillDetailsQueryHandler : IRequestHandler<GetSkillDetailsQuery,SkillViewModel>
    {
        private readonly ISkillRepository repository;
        private readonly IMapper mapper;

        public GetSkillDetailsQueryHandler(ISkillRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<SkillViewModel> Handle(GetSkillDetailsQuery request, CancellationToken cancellationToken)
        {

            #region fetch skill details 

            var skillDetails = await repository.GetSkillByIdAsync(request.EmployeeId, request.SkillId);

            #endregion
            
            #region check if skill exists

            if (skillDetails == null)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            #endregion

            return mapper.Map<SkillViewModel>(skillDetails);
        }
    }
}
