using AutoMapper;
using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Application.Dtos.ResourceRequests;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Application.Dtos.WorkExperience;
using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree;
using CVBuilder.Application.Features.Degrees.Commands.UpdateDegree;
using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject;
using CVBuilder.Application.Features.Projects.Commands.UpdateProject;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddWorkExperienceRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateWorkExperienceRequest;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill;
using CVBuilder.Application.Features.Skills.Commands.UpdateSkill;
using CVBuilder.Application.Features.UpdatePassword.Commands;
using CVBuilder.Application.Features.UpdatePersonalDetails.Commands;
using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using CVBuilder.Application.ViewModels.Account;
using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Application.ViewModels.Employee;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.UpdateResourceRequest;
using CVBuilder.Application.ViewModels.WorkExperience;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;

namespace CVBuilder.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee Mappings
            CreateMap<Employee, EmployeesListDto>()
                .ForMember(e => e.Role, op => op.MapFrom(o => o.Role));
            CreateMap<Employee, EmployeeDetailsDto>();
            CreateMap<AddEmployeeViewModel, AddEmployeeCommand>();
            CreateMap<AddEmployeeCommand, Employee>();
            CreateMap<Employee, AddEmployeeCommandResponse>();
            CreateMap<UpdateEmployeeViewModel, UpdateEmployeeCommand>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            CreateMap<PartialUpdateEmployeeCommand, Employee>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            
            // Skill Mappings
            CreateMap<Skill, SkillDetailsDto>();
            CreateMap<Skill, SkillsListDto>();
            CreateMap<SkillViewModel, AddSkillCommand>();
            CreateMap<AddSkillCommand, Skill>();
            CreateMap<Skill, AddSkillCommandResponse>();
            CreateMap<SkillViewModel, UpdateSkillCommand>();
            CreateMap<UpdateSkillCommand, Skill>();
            CreateMap<PartialUpdateSkillCommand, Skill>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // Degree Mappings
            CreateMap<Degree, DegreeDetailsDto>();
            CreateMap<Degree, DegreesListDto>();
            CreateMap<DegreeViewModel, AddDegreeCommand>();
            CreateMap<AddDegreeCommand, Degree>();
            CreateMap<Degree, AddDegreeCommandResponse>();
            CreateMap<DegreeViewModel, UpdateDegreeCommand>();
            CreateMap<UpdateDegreeCommand, Degree>();
            CreateMap<PartialUpdateDegreeCommand, Degree>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            // Project Mappings
            CreateMap<Project, ProjectDetailsDto>();
            CreateMap<Project, ProjectsListDto>();
            CreateMap<ProjectViewModel, AddProjectCommand>();
            CreateMap<AddProjectCommand, Project>();
            CreateMap<Project, AddProjectCommandResponse>();
            CreateMap<ProjectViewModel, UpdateProjectCommand>();
            CreateMap<UpdateProjectCommand, Project>();
            CreateMap<PartialUpdateProjectCommand, Project>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Work Experince Mappings
            CreateMap<WorkExperience, WorkExperienceDetailsDto>();
            CreateMap<WorkExperience, WorkExperiencesListDto>();
            CreateMap<WorkExperienceViewModel, AddWorkExperienceCommand>();
            CreateMap<AddWorkExperienceCommand, WorkExperience>();
            CreateMap<WorkExperience, AddWorkExperienceCommandResponse>();
            CreateMap<WorkExperienceViewModel, UpdateWorkExperienceCommand>();
            CreateMap<UpdateWorkExperienceCommand, WorkExperience>();
            CreateMap<PartialUpdateWorkExperienceCommand, WorkExperience>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Update Password Mappings
            CreateMap<UpdatePasswordViewModel, UpdatePasswordCommand>();


            // Personal Details Update Mappings
            CreateMap<UpdatePersonalDetailsCommand, PersonalDetailsUpdateRequest>();
            CreateMap<PersonalDetailsUpdateRequest, UpdatePersonalDetailsCommandResponse>();
            CreateMap<PersonalDetailsUpdateRequest, PersonalDetailsUpdateRequestDto>();

            // Add Degree Request Mappings
            CreateMap<AddDegreeRequestViewModel, AddDegreeRequestCommand>();
            CreateMap<AddDegreeRequestCommand, DegreeUpdateRequest>();
            CreateMap<ResourceRequest, AddDegreeRequestCommandResponse>();
            CreateMap<DegreeUpdateRequest, DegreeUpdateRequestDto>();

            // Update Degree Request Mappings
            CreateMap<UpdateDegreeRequestCommand, DegreeUpdateRequest>();
            CreateMap<ResourceRequest, UpdateDegreeRequestCommandResponse>();

            // Add Project Request Mappings
            CreateMap<AddProjectRequestViewModel, AddProjectRequestCommand>();
            CreateMap<AddProjectRequestCommand, ProjectUpdateRequest>();
            CreateMap<ResourceRequest, AddProjectRequestCommandResponse>();
            CreateMap<ProjectUpdateRequest, ProjectUpdateRequestDto>();
            // Update Project Request Mappings
            CreateMap<UpdateProjectRequestCommand, ProjectUpdateRequest>();
            CreateMap<ResourceRequest, UpdateProjectRequestCommandResponse>();

            // Add Skill Request Mappings
            CreateMap<AddSkillRequestViewModel, AddSkillRequestCommand>();
            CreateMap<AddSkillRequestCommand, SkillUpdateRequest>();
            CreateMap<ResourceRequest, AddSkillRequestCommandResponse>();
            CreateMap<SkillUpdateRequest, SkillUpdateRequestDto>();

            // Update Skill Request Mappings
            CreateMap<UpdateSkillRequestCommand, SkillUpdateRequest>();
            CreateMap<ResourceRequest, UpdateSkillRequestCommandResponse>();

            // Add Work Experience Request Mappings
            CreateMap<AddWorkExperienceRequestViewModel, AddWorkExperienceRequestCommand>();
            CreateMap<AddWorkExperienceRequestCommand, WorkExperienceUpdateRequest>();
            CreateMap<ResourceRequest, AddWorkExperienceRequestCommandResponse>();
            CreateMap<WorkExperienceUpdateRequest, WorkExperienceUpdateRequestDto>();

            // Update Work Experience Request Mappings
            CreateMap<UpdateWorkExperienceRequestCommand, WorkExperienceUpdateRequest>();
            CreateMap<ResourceRequest, UpdateWorkExperienceRequestCommandResponse>();

            // Resource Requests Mappings
            CreateMap<ResourceRequest, ResourceRequestsListDto>();
            CreateMap<ResourceRequest, UpdatePersonalDetailsCommandResponse>();
            CreateMap<ResourceRequest, ResourceRequestDetailsDto>();

            
        }
    }
}

