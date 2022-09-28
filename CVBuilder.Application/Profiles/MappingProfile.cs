﻿using AutoMapper;
using CVBuilder.Application.Features.Employees.Commands.AddEmployee;
using CVBuilder.Application.Features.Employees.Commands.UpdateEmployee;
using CVBuilder.Application.Features.Skills.Commands.AddSkill;
using CVBuilder.Application.Features.Skills.Commands.UpdateSkill;
using CVBuilder.Application.ViewModels;
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
            CreateMap<AddSkillCommand, Skill>();
            CreateMap<Skill, AddSkillCommandResponse>();
            CreateMap<UpdateSkillCommand, Skill>();
        }
    }
}

