﻿using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;

namespace CVBuilder.Application.ViewModels
{
    public class EmployeeDetailViewModel
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }

        public ICollection<SkillViewModel> Skills { get; set; }
        public ICollection<DegreeViewModel> Degrees { get; set; }
        public ICollection<WorkExperienceViewModel> WorkExperiences { get; set; }
        public ICollection<ProjectViewModel> Projects { get; set; }

    }
}
