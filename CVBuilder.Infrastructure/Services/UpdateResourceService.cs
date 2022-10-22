using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateResourceService : IUpdateResourceService
    {
        private readonly IUpdateDegreeService updateDegreeService;
        private readonly IUpdatePersonalDetailsService updatePersonalDetailsService;
        private readonly IUpdateProjectService updateProjectService;
        private readonly IUpdateSkillService updateSkillService;
        private readonly IUpdateWorkExperienceService updateWorkExperienceService;

        public UpdateResourceService(IUpdateDegreeService updateDegreeService, IUpdatePersonalDetailsService updatePersonalDetailsService ,IUpdateProjectService updateProjectService, IUpdateSkillService updateSkillService, IUpdateWorkExperienceService updateWorkExperienceService)
        {
            this.updateDegreeService = updateDegreeService;
            this.updatePersonalDetailsService = updatePersonalDetailsService;
            this.updateProjectService = updateProjectService;
            this.updateSkillService = updateSkillService;
            this.updateWorkExperienceService = updateWorkExperienceService;
        }

        public async Task UpdateResource(ResourceRequest resourceRequest)
        {
            if (resourceRequest.ResourceType == ResourceType.Degree.ToString())
            {
                await updateDegreeService.UpdateResource(resourceRequest);
            }
            
            else if (resourceRequest.ResourceType == ResourceType.PersonalDetails.ToString())
            {
                await updatePersonalDetailsService.UpdateResource(resourceRequest);
            }

            else if (resourceRequest.ResourceType == ResourceType.Project.ToString())
            {
                await updateProjectService.UpdateResource(resourceRequest);
            }

            else if (resourceRequest.ResourceType == ResourceType.Skill.ToString())
            {
                await updateSkillService.UpdateResource(resourceRequest);
            }

            else if (resourceRequest.ResourceType == ResourceType.WorkExperience.ToString())
            {
                await updateWorkExperienceService.UpdateResource(resourceRequest);
            }
        }
    }
}
