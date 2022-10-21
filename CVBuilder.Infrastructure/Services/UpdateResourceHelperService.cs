using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceHelper;
using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree;
using CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee;
using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill;
using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateResourceHelperService : IUpdateResourceHelperService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UpdateResourceHelperService(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task AddDegree(ResourceRequest resourceRequest, DegreeUpdateRequest resourceDetails)
        {
            var requestDto = new AddDegreeCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task AddProject(ResourceRequest resourceRequest, ProjectUpdateRequest resourceDetails)
        {
            var requestDto = new AddProjectCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task AddSkill(ResourceRequest resourceRequest, SkillUpdateRequest resourceDetails)
        {
            var requestDto = new AddSkillCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task AddWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest resourceDetails)
        {
            var requestDto = new AddWorkExperienceCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task UpdateDegree(ResourceRequest resourceRequest, DegreeUpdateRequest resourceDetails)
        {
            var requestDto = new PartialUpdateDegreeCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task UpdatePersonalDetails(ResourceRequest resourceRequest, PersonalDetailsUpdateRequest resourceDetails)
        {
            var requestDto = new PartialUpdateEmployeeCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task UpdateProject(ResourceRequest resourceRequest, ProjectUpdateRequest resourceDetails)
        {
            var requestDto = new PartialUpdateProjectCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task UpdateSkill(ResourceRequest resourceRequest, SkillUpdateRequest resourceDetails)
        {
            var requestDto = new PartialUpdateSkillCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }

        public async Task UpdateWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest resourceDetails)
        {
            var requestDto = new PartialUpdateWorkExperienceCommand();

            mapper.Map(resourceDetails, requestDto);

            requestDto.EmployeeId = resourceRequest.AppliedBy;

            await mediator.Send(requestDto);
        }
    }
}
