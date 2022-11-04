using AutoMapper;
using CVBuilder.Application.Dtos.Company;
using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Application.Dtos.Email;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Application.Dtos.ResourceRequests;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Application.Dtos.UpdateCVRequestServiceResponse;
using CVBuilder.Application.Dtos.WorkExperience;
using CVBuilder.Application.Features.Company.Commands.AddCompany;
using CVBuilder.Application.Features.Company.Commands.UpdateCompany;
using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.DeleteDegree;
using CVBuilder.Application.Features.Degrees.Commands.PartialUpdateDegree;
using CVBuilder.Application.Features.Degrees.Commands.UpdateDegree;
using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.PartialUpdateEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.DeleteProject;
using CVBuilder.Application.Features.Projects.Commands.PartialUpdateProject;
using CVBuilder.Application.Features.Projects.Commands.UpdateProject;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.AddResourceRequest.AddWorkExperienceRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.DeleteResourceRequest.DeleteWorkExperienceRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateDegreeRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateProjectRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateSkillRequest;
using CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateWorkExperienceRequest;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.DeleteSkill;
using CVBuilder.Application.Features.Skills.Commands.PartialUpdateSkill;
using CVBuilder.Application.Features.Skills.Commands.UpdateSkill;
using CVBuilder.Application.Features.UpdatePassword.Commands;
using CVBuilder.Application.Features.UpdatePersonalDetails.Commands;
using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.DeleteWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.PartialUpdateWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using CVBuilder.Application.ViewModels.Account;
using CVBuilder.Application.ViewModels.Company;
using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Application.ViewModels.Employee;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.SendEmail;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.UpdateResourceRequest;
using CVBuilder.Application.ViewModels.WorkExperience;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.ValueObjects;

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
            CreateMap<Skill, SkillDetailsDto>()
                .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, SkillDetailsDto>();

            CreateMap<Skill, SkillsListDto>()
                .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, SkillsListDto>();

            CreateMap<SkillViewModel, AddSkillCommand>();

            CreateMap<AddSkillCommand, Skill>()
                .ForMember(dest => dest.SkillDetails, src => src.MapFrom(src => new SkillDetails(src.Name)));

            CreateMap<Skill, AddSkillCommandResponse>()
                .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, AddSkillCommandResponse>();

            CreateMap<SkillViewModel, UpdateSkillCommand>();

            CreateMap<UpdateSkillCommand, Skill>()
                 .ForMember(dest => dest.SkillDetails, src => src.MapFrom(src => new SkillDetails(src.Name)));

            CreateMap<PartialUpdateSkillCommand, Skill>()
                .ForMember(dest => dest.SkillDetails, src => src.MapFrom(src => new SkillDetails(src.Name)));


            // Degree Mappings
            CreateMap<Degree, DegreeDetailsDto>()
                .IncludeMembers(src => src.DegreeDetails);
            
            CreateMap<DegreeDetails, DegreeDetailsDto>();

            CreateMap<Degree, DegreesListDto>()
                .IncludeMembers(src => src.DegreeDetails);

            CreateMap<DegreeDetails, DegreesListDto>();
            
            CreateMap<DegreeViewModel, AddDegreeCommand>();

            CreateMap<AddDegreeCommand, Degree>()
                .ForMember(dest => dest.DegreeDetails, src => src.MapFrom(src => new DegreeDetails(src.Name, src.Subject, src.Institute)));

            CreateMap<Degree, AddDegreeCommandResponse>()
                .IncludeMembers(src => src.DegreeDetails);

            CreateMap<DegreeDetails, AddDegreeCommandResponse>();
            
            CreateMap<DegreeViewModel, UpdateDegreeCommand>();

            CreateMap<UpdateDegreeCommand, Degree>()
                .ForMember(dest => dest.DegreeDetails, src => src.MapFrom(src => new DegreeDetails(src.Name, src.Subject, src.Institute)));

            CreateMap<PartialUpdateDegreeCommand, Degree>()
                .ForMember(dest => dest.DegreeDetails, src => src.MapFrom(src => new DegreeDetails(src.Name, src.Subject, src.Institute)));


            // Project Mappings
            CreateMap<Project, ProjectDetailsDto>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, ProjectDetailsDto>();

            CreateMap<Project, ProjectsListDto>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, ProjectsListDto>();

            CreateMap<ProjectViewModel, AddProjectCommand>();

            CreateMap<AddProjectCommand, Project>()
                .ForMember(dest => dest.ProjectDetails, src => src.MapFrom(src => new ProjectDetails(src.Name, src.Description, src.Link)));

            CreateMap<Project, AddProjectCommandResponse>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, AddProjectCommandResponse>();

            CreateMap<ProjectViewModel, UpdateProjectCommand>();

            CreateMap<UpdateProjectCommand, Project>()
                .ForMember(dest => dest.ProjectDetails, src => src.MapFrom(src => new ProjectDetails(src.Name, src.Description, src.Link)));

            CreateMap<PartialUpdateProjectCommand, Project>()
                .ForMember(dest => dest.ProjectDetails, src => src.MapFrom(src => new ProjectDetails(src.Name, src.Description, src.Link)));

            // Work Experience Mappings
            CreateMap<WorkExperience, WorkExperienceDetailsDto>()
                .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, WorkExperienceDetailsDto>();

            CreateMap<WorkExperience, WorkExperiencesListDto>()
                .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, WorkExperiencesListDto>();

            CreateMap<WorkExperienceViewModel, AddWorkExperienceCommand>();

            CreateMap<AddWorkExperienceCommand, WorkExperience>()
                .ForMember(dest => dest.WorkExperienceDetails, src => src.MapFrom(src => new WorkExperienceDetails(src.Designation, src.Company, src.StartDate, src.EndDate)));

            CreateMap<WorkExperience, AddWorkExperienceCommandResponse>()
                .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, AddWorkExperienceCommandResponse>();

            CreateMap<WorkExperienceViewModel, UpdateWorkExperienceCommand>();

            CreateMap<UpdateWorkExperienceCommand, WorkExperience>()
                .ForMember(dest => dest.WorkExperienceDetails, src => src.MapFrom(src => new WorkExperienceDetails(src.Designation, src.Company, src.StartDate, src.EndDate)));

            CreateMap<PartialUpdateWorkExperienceCommand, WorkExperience>()
                .ForMember(dest => dest.WorkExperienceDetails, src => src.MapFrom(src => new WorkExperienceDetails(src.Designation, src.Company, src.StartDate, src.EndDate)));


            // Update Password Mappings
            CreateMap<UpdatePasswordViewModel, UpdatePasswordCommand>();


            // Personal Details Update Mappings
            CreateMap<UpdatePersonalDetailsCommand, PersonalDetailsUpdateRequest>();
            CreateMap<PersonalDetailsUpdateRequest, UpdatePersonalDetailsCommandResponse>();
            CreateMap<PersonalDetailsUpdateRequest, PersonalDetailsUpdateRequestDto>();
            CreateMap<PersonalDetailsUpdateRequest, PartialUpdateEmployeeCommand>();


            // Degree Request Mappings
            CreateMap<DegreeUpdateRequest, DegreeUpdateRequestDto>()
                .IncludeMembers(src => src.DegreeDetails);

            CreateMap<DegreeDetails, DegreeUpdateRequestDto>();

            CreateMap<AddDegreeRequestViewModel, AddDegreeRequestCommand>();

            CreateMap<AddDegreeRequestCommand, DegreeUpdateRequest>()
                .ForMember(dest => dest.DegreeDetails, src => src.MapFrom(src => new DegreeDetails(src.Name, src.Subject, src.Institute)));

            CreateMap<ResourceRequest, AddDegreeRequestCommandResponse>();

            CreateMap<DegreeUpdateRequest, AddDegreeCommand>()
                .IncludeMembers(src => src.DegreeDetails);

            CreateMap<DegreeDetails, AddDegreeCommand>();

            CreateMap<UpdateDegreeRequestCommand, DegreeUpdateRequest>()
                .ForMember(dest => dest.DegreeDetails, src => src.MapFrom(src => new DegreeDetails(src.Name, src.Subject, src.Institute)));

            CreateMap<ResourceRequest, UpdateDegreeRequestCommandResponse>();

            CreateMap<DegreeUpdateRequest, UpdateDegreeCommand>()
                .IncludeMembers(src => src.DegreeDetails);

            CreateMap<DegreeDetails, UpdateDegreeCommand>();

            CreateMap<DegreeUpdateRequest, PartialUpdateDegreeCommand>()
                .IncludeMembers(src => src.DegreeDetails);

            CreateMap<DegreeDetails, PartialUpdateDegreeCommand>();

            CreateMap<DeleteDegreeRequestCommand, DegreeUpdateRequest>();

            CreateMap<ResourceRequest, DeleteDegreeRequestCommandResponse>();

            CreateMap<DegreeUpdateRequest, DeleteDegreeCommand>();



            // Project Request Mappings
            CreateMap<ProjectUpdateRequest, ProjectUpdateRequestDto>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, ProjectUpdateRequestDto>();

            CreateMap<AddProjectRequestViewModel, AddProjectRequestCommand>();

            CreateMap<AddProjectRequestCommand, ProjectUpdateRequest>()
                .ForMember(dest => dest.ProjectDetails, src => src.MapFrom(src => new ProjectDetails(src.Name, src.Description, src.Link)));

            CreateMap<ResourceRequest, AddProjectRequestCommandResponse>();
            
            CreateMap<ProjectUpdateRequest, AddProjectCommand>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, AddProjectCommand>();

            CreateMap<UpdateProjectRequestCommand, ProjectUpdateRequest>()
                .ForMember(dest => dest.ProjectDetails, src => src.MapFrom(src => new ProjectDetails(src.Name, src.Description, src.Link)));

            CreateMap<ResourceRequest, UpdateProjectRequestCommandResponse>();

            CreateMap<ProjectUpdateRequest, UpdateProjectCommand>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, UpdateProjectCommand>();

            CreateMap<ProjectUpdateRequest, PartialUpdateProjectCommand>()
                .IncludeMembers(src => src.ProjectDetails);

            CreateMap<ProjectDetails, PartialUpdateProjectCommand>();

            CreateMap<DeleteProjectRequestCommand, ProjectUpdateRequest>();

            CreateMap<ResourceRequest, DeleteProjectRequestCommandResponse>();

            CreateMap<ProjectUpdateRequest, DeleteProjectCommand>();


            // Skill Request Mappings
            CreateMap<SkillUpdateRequest, SkillUpdateRequestDto>()
                .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, SkillUpdateRequestDto>();

            CreateMap<AddSkillRequestViewModel, AddSkillRequestCommand>();

            CreateMap<AddSkillRequestCommand, SkillUpdateRequest>()
                .ForMember(dest => dest.SkillDetails, src => src.MapFrom(src => new SkillDetails(src.Name)));

            CreateMap<ResourceRequest, AddSkillRequestCommandResponse>();
            
            CreateMap<SkillUpdateRequest, AddSkillCommand>()
                .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, AddSkillCommand>();

            CreateMap<UpdateSkillRequestCommand, SkillUpdateRequest>()
                .ForMember(dest => dest.SkillDetails, src => src.MapFrom(src => new SkillDetails(src.Name)));

            CreateMap<ResourceRequest, UpdateSkillRequestCommandResponse>();

            CreateMap<SkillUpdateRequest, UpdateSkillCommand>()
               .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, UpdateSkillCommand>();

            CreateMap<SkillUpdateRequest, PartialUpdateSkillCommand>()
                .IncludeMembers(src => src.SkillDetails);

            CreateMap<SkillDetails, PartialUpdateSkillCommand>();

            CreateMap<DeleteSkillRequestCommand, SkillUpdateRequest>();

            CreateMap<ResourceRequest, DeleteSkillRequestCommandResponse>();

            CreateMap<SkillUpdateRequest, DeleteSkillCommand>();


            // Work Experience Request Mappings
            CreateMap<WorkExperienceUpdateRequest, WorkExperienceUpdateRequestDto>()
                .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, WorkExperienceUpdateRequestDto>();

            CreateMap<AddWorkExperienceRequestViewModel, AddWorkExperienceRequestCommand>();

            CreateMap<AddWorkExperienceRequestCommand, WorkExperienceUpdateRequest>()
                .ForMember(dest => dest.WorkExperienceDetails, src => src.MapFrom(src => new WorkExperienceDetails(src.Designation, src.Company, src.StartDate, src.EndDate)));

            CreateMap<ResourceRequest, AddWorkExperienceRequestCommandResponse>();

            CreateMap<WorkExperienceUpdateRequest, AddWorkExperienceCommand>()
                .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, AddWorkExperienceCommand>();

            CreateMap<UpdateWorkExperienceRequestCommand, WorkExperienceUpdateRequest>()
                .ForMember(dest => dest.WorkExperienceDetails, src => src.MapFrom(src => new WorkExperienceDetails(src.Designation, src.Company, src.StartDate, src.EndDate)));

            CreateMap<ResourceRequest, UpdateWorkExperienceRequestCommandResponse>();

            CreateMap<WorkExperienceUpdateRequest, UpdateWorkExperienceCommand>()
               .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, UpdateWorkExperienceCommand>();

            CreateMap<WorkExperienceUpdateRequest, PartialUpdateWorkExperienceCommand>()
                .IncludeMembers(src => src.WorkExperienceDetails);

            CreateMap<WorkExperienceDetails, PartialUpdateWorkExperienceCommand>();

            CreateMap<DeleteWorkExperienceRequestCommand, WorkExperienceUpdateRequest>();

            CreateMap<ResourceRequest, DeleteWorkExperienceRequestCommandResponse>();

            CreateMap<WorkExperienceUpdateRequest, DeleteWorkExperienceCommand>();


            // Resource Requests Mappings
            CreateMap<ResourceRequest, ResourceRequestsListDto>();
            CreateMap<ResourceRequest, UpdatePersonalDetailsCommandResponse>();
            CreateMap<ResourceRequest, ResourceRequestDetailsDto>();


            // Email View Model Mappings
            CreateMap<EmailViewModel, EmailDto>();

            // Update CV Mappings
            CreateMap<UpdateDegreeViewModel, AddDegreeRequestCommand>();
            CreateMap<AddDegreeRequestCommandResponse, Response>();

            CreateMap<UpdateDegreeViewModel, UpdateDegreeRequestCommand>();
            CreateMap<UpdateDegreeRequestCommandResponse, Response>();

            CreateMap<UpdateDegreeViewModel, DeleteDegreeRequestCommand>();
            CreateMap<DeleteDegreeRequestCommandResponse, Response>();

            CreateMap<UpdateProjectViewModel, AddProjectRequestCommand>();
            CreateMap<AddProjectRequestCommandResponse, Response>();

            CreateMap<UpdateProjectViewModel, UpdateProjectRequestCommand>();
            CreateMap<UpdateProjectRequestCommandResponse, Response>();

            CreateMap<UpdateProjectViewModel, DeleteProjectRequestCommand>();
            CreateMap<DeleteProjectRequestCommandResponse, Response>();

            CreateMap<UpdateSkillViewModel, AddSkillRequestCommand>();
            CreateMap<AddSkillRequestCommandResponse, Response>();

            CreateMap<UpdateSkillViewModel, UpdateSkillRequestCommand>();
            CreateMap<UpdateSkillRequestCommandResponse, Response>();

            CreateMap<UpdateSkillViewModel, DeleteSkillRequestCommand>();
            CreateMap<DeleteSkillRequestCommandResponse, Response>();

            CreateMap<UpdateWorkExperienceViewModel, AddWorkExperienceRequestCommand>();
            CreateMap<AddWorkExperienceRequestCommandResponse, Response>();

            CreateMap<UpdateWorkExperienceViewModel, UpdateWorkExperienceRequestCommand>();
            CreateMap<UpdateWorkExperienceRequestCommandResponse, Response>();

            CreateMap<UpdateWorkExperienceViewModel, DeleteWorkExperienceRequestCommand>();
            CreateMap<DeleteWorkExperienceRequestCommandResponse, Response>();


            // Company Mappings
            CreateMap<RegisterViewModel, AddCompanyCommand>();
            CreateMap<AddCompanyCommand, Company>();
            CreateMap<Company, AddCompanyCommandResponse>()
                .IncludeMembers(c=> c.CompanyDetails)
                .ForMember(dest => dest.SubscriptionType, src => src.MapFrom(src => src.SubscriptionType));

            CreateMap<Company, CompanyDetailsDto>()
                .IncludeMembers(c => c.CompanyDetails)
                .ForMember(dest => dest.SubscriptionType, src => src.MapFrom(src => src.SubscriptionType));

            CreateMap<UpdateCompanyDetailsViewModel, UpdateCompanyDetailsCommand>();

            // Company Details Mappings
            CreateMap<AddCompanyCommand, CompanyDetails>();
            CreateMap<CompanyDetails, AddCompanyCommandResponse>();
            CreateMap<CompanyDetails, CompanyDetailsDto>();
            CreateMap<UpdateCompanyDetailsCommand, CompanyDetails>();
                
        }
    }
}

