﻿using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.WorkExperience;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperiencesList
{
    public class GetWorkExperiencesListQueryHandler : IRequestHandler<GetWorkExperiencesListQuery, List<WorkExperienceDetailsDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWorkExperienceRepository workExperienceRepository;
        private readonly IMapper mapper;

        public GetWorkExperiencesListQueryHandler(IEmployeeRepository employeeRepository, IWorkExperienceRepository workExperienceRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.workExperienceRepository = workExperienceRepository;
            this.mapper = mapper;
        }
        public async Task<List<WorkExperienceDetailsDto>> Handle(GetWorkExperiencesListQuery request, CancellationToken cancellationToken)
        {
            #region check if employee exists

            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            #endregion

            #region fetch work experiences list

            var workExperiencesList = await workExperienceRepository.ListAllAsync(e => e.EmployeeId == request.EmployeeId);

            #endregion

            var resultDtos = mapper.Map<List<WorkExperienceDetailsDto>>(workExperiencesList);

            return resultDtos;
        }
    }
}
