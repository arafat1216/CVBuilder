using AutoMapper;
using CVBuilder.Application.Dtos.Degree;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Dtos.Project;
using CVBuilder.Application.Dtos.Skill;
using CVBuilder.Application.Dtos.WorkExperience;
using CVBuilder.Application.Features.Degrees.Commands.AddDegree;
using CVBuilder.Application.Features.Degrees.Commands.UpdateDegree;
using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Projects.Commands.AddProject;
using CVBuilder.Application.Features.Projects.Commands.UpdateProject;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.UpdateSkill;
using CVBuilder.Application.Features.WorkExperiences.Commands.AddWorkExperience;
using CVBuilder.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee Mappings
            CreateMap<Employee, EmployeesListDto>();
            CreateMap<Employee, EmployeeDetailsDto>();
            CreateMap<AddEmployeeCommand, Employee>();
            CreateMap<Employee, AddEmployeeCommandResponse>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            

            // Skill Mappings
            CreateMap<Skill, SkillDetailsDto>();
            CreateMap<Skill, SkillsListDto>();
            CreateMap<SkillViewModel, AddSkillCommand>();
            CreateMap<AddSkillCommand, Skill>();
            CreateMap<Skill, AddSkillCommandResponse>();
            CreateMap<SkillViewModel, UpdateSkillCommand>();
            CreateMap<UpdateSkillCommand, Skill>();


            // Degree Mappings
            CreateMap<Degree, DegreeDetailsDto>();
            CreateMap<Degree, DegreesListDto>();
            CreateMap<DegreeViewModel, AddDegreeCommand>();
            CreateMap<AddDegreeCommand, Degree>();
            CreateMap<Degree, AddDegreeCommandResponse>();
            CreateMap<DegreeViewModel, UpdateDegreeCommand>();
            CreateMap<UpdateDegreeCommand, Degree>();
            


            // Project Mappings
            CreateMap<Project, ProjectDetailsDto>();
            CreateMap<Project, ProjectsListDto>();
            CreateMap<ProjectViewModel, AddProjectCommand>();
            CreateMap<AddProjectCommand, Project>();
            CreateMap<Project, AddProjectCommandResponse>();
            CreateMap<ProjectViewModel, UpdateProjectCommand>();
            CreateMap<UpdateProjectCommand, Project>();


            // Work Experince Mappings
            CreateMap<WorkExperience, WorkExperienceDetailsDto>();
            CreateMap<WorkExperience, WorkExperiencesListDto>();
            CreateMap<AddWorkExperienceCommand, WorkExperience>();
            CreateMap<WorkExperience, AddWorkExperienceCommandResponse>();
            CreateMap<UpdateWorkExperienceCommand, WorkExperience>();
        }
    }
}

