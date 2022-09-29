﻿using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.ViewModels;
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

            #region check if skill exists

            var skillExists = await repository.ExistsAsync(request.EmployeeId, request.SkillId);

            if (!skillExists)
                throw new Exceptions.NotFoundException(nameof(Skill), request.SkillId);

            #endregion

            var skillDetails = await repository.GetSkillByIdAsync(request.EmployeeId, request.SkillId);

            return mapper.Map<SkillViewModel>(skillDetails);
        }
    }
}