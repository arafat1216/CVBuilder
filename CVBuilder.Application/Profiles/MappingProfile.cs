using AutoMapper;
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
using CVBuilder.Application.ViewModels.Employee;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.WorkExperience;
using CVBuilder.Domain.Entities;

namespace CVBuilder.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee Mappings
            CreateMap<Employee, EmployeeListViewModel>();
            CreateMap<Employee, EmployeeDetailViewModel>();
            CreateMap<AddEmployeeCommand, Employee>();
            CreateMap<Employee, AddEmployeeCommandResponse>();
            CreateMap<UpdateEmployeeCommand, Employee>();
            

            // Skill Mappings
            CreateMap<Skill, SkillViewModel>();
            CreateMap<Skill, SkillsListViewModel>();
            CreateMap<AddSkillCommand, Skill>();
            CreateMap<Skill, AddSkillCommandResponse>();
            CreateMap<UpdateSkillCommand, Skill>();


            // Degree Mappings
            CreateMap<Degree, DegreeViewModel>();
            CreateMap<Degree, DegreesListViewModel>();
            CreateMap<AddDegreeCommand, Degree>();
            CreateMap<Degree, AddDegreeCommandResponse>();
            CreateMap<UpdateDegreeCommand, Degree>();


            // Project Mappings
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Project, ProjectsListViewModel>();
            CreateMap<AddProjectCommand, Project>();
            CreateMap<Project, AddProjectCommandResponse>();
            CreateMap<UpdateProjectCommand, Project>();


            // Work Experince Mappings
            CreateMap<WorkExperience, WorkExperienceViewModel>();
            CreateMap<WorkExperience, WorkExperiencesListViewModel>();
            CreateMap<AddWorkExperienceCommand, WorkExperience>();
            CreateMap<WorkExperience, AddWorkExperienceCommandResponse>();
            CreateMap<UpdateWorkExperienceCommand, WorkExperience>();
        }
    }
}

