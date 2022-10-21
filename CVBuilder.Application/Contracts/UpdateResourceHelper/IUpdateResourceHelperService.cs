using CVBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBuilder.Application.Contracts.UpdateResourceHelper
{
    public interface IUpdateResourceHelperService
    {
        Task AddDegree(ResourceRequest resourceRequest, DegreeUpdateRequest resourceDetails);
        Task AddProject(ResourceRequest resourceRequest, ProjectUpdateRequest resourceDetails);
        Task AddSkill(ResourceRequest resourceRequest, SkillUpdateRequest resourceDetails);
        Task AddWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest resourceDetails);
        Task UpdateDegree(ResourceRequest resourceRequest, DegreeUpdateRequest resourceDetails);
        Task UpdatePersonalDetails(ResourceRequest resourceRequest, PersonalDetailsUpdateRequest resourceDetails);
        Task UpdateProject(ResourceRequest resourceRequest, ProjectUpdateRequest resourceDetails);
        Task UpdateSkill(ResourceRequest resourceRequest, SkillUpdateRequest resourceDetails);
        Task UpdateWorkExperience(ResourceRequest resourceRequest, WorkExperienceUpdateRequest resourceDetails);
    }
}
