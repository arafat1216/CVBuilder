using AutoMapper;
using CVBuilder.Application.Contracts.UpdateCVRequest;
using CVBuilder.Application.Dtos.UpdateCVRequestServiceResponse;
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
using CVBuilder.Application.ViewModels.Degree;
using CVBuilder.Application.ViewModels.Project;
using CVBuilder.Application.ViewModels.Skill;
using CVBuilder.Application.ViewModels.UpdateCV;
using CVBuilder.Application.ViewModels.WorkExperience;
using MediatR;

namespace CVBuilder.Infrastructure.Services
{
    public class UpdateCVRequestService : IUpdateCVRequestService
    {

        public UpdateCVRequestService(IMediator mediator, IMapper mapper)
        {
            ResponsesList = new List<Response>();
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public List<Response> ResponsesList { get; set; }
       
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public async Task<List<Response>> HandleRequest(UpdateCVViewModel updateCVViewModel)
        {
            
            if (updateCVViewModel.Degrees != null)
                 await ProcessDegreeRequest(updateCVViewModel.Degrees);

            if (updateCVViewModel.Projects != null)
                await ProcessProjectRequest(updateCVViewModel.Projects);

            if (updateCVViewModel.Skills != null)
                await ProcessSkillRequest(updateCVViewModel.Skills);

            if (updateCVViewModel.WorkExperiences != null)
                await ProcessWorkExperienceRequest(updateCVViewModel.WorkExperiences);

            return ResponsesList;
        }

        private async Task ProcessWorkExperienceRequest(List<UpdateWorkExperienceViewModel> workExperiences)
        {
            foreach (var workExperience in workExperiences)
            {
                if ((workExperience.WorkExperienceId == null) && (workExperience.Designation != null))
                {
                    await AddWorkExperienceRequest(workExperience);
                }

                else if ((workExperience.WorkExperienceId != null) && (workExperience.Designation != null))
                {
                    await UpdateWorkExperienceRequest(workExperience);
                }

                else if ((workExperience.WorkExperienceId != null) && (workExperience.Designation == null))
                {
                    await DeleteWorkExperienceRequest(workExperience);
                }
            }
        }

        private async Task DeleteWorkExperienceRequest(UpdateWorkExperienceViewModel workExperience)
        {
            var requestDto = mapper.Map<DeleteWorkExperienceRequestCommand>(workExperience);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task UpdateWorkExperienceRequest(UpdateWorkExperienceViewModel workExperience)
        {
            var requestDto = mapper.Map<UpdateWorkExperienceRequestCommand>(workExperience);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task AddWorkExperienceRequest(UpdateWorkExperienceViewModel workExperience)
        {
            var requestDto = mapper.Map<AddWorkExperienceRequestCommand>(workExperience);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task ProcessSkillRequest(List<UpdateSkillViewModel> skills)
        {
            foreach (var skill in skills)
            {
                if ((skill.SkillId == null) && (skill.Name != null))
                {
                    await AddSkillRequest(skill);
                }
                else if ((skill.SkillId != null) && (skill.Name != null))
                {
                    await UpdateSkillRequest(skill);
                }
                else if ((skill.SkillId != null) && (skill.Name == null))
                {
                    await DeleteSkillRequest(skill);
                }
            }
        }

        private async Task DeleteSkillRequest(UpdateSkillViewModel skill)
        {
            var requestDto = mapper.Map<DeleteSkillRequestCommand>(skill);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task UpdateSkillRequest(UpdateSkillViewModel skill)
        {
            var requestDto = mapper.Map<UpdateSkillRequestCommand>(skill);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task AddSkillRequest(UpdateSkillViewModel skill)
        {
            var requestDto = mapper.Map<AddSkillRequestCommand>(skill);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task ProcessProjectRequest(List<UpdateProjectViewModel> projects)
        {
            foreach (var project in projects)
            {
                if ((project.ProjectId == null) && (project.Name != null))
                {
                    await AddProjectRequest(project);
                }

                else if ((project.ProjectId != null) && (project.Name != null))
                {
                    await UpdateProjectRequest(project);
                }

                else if ((project.ProjectId != null) && (project.Name == null))
                {
                    await DeleteProjectRequest(project);
                }
            }
        }

        private async Task DeleteProjectRequest(UpdateProjectViewModel project)
        {
            var requestDto = mapper.Map<DeleteProjectRequestCommand>(project);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task UpdateProjectRequest(UpdateProjectViewModel project)
        {
            var requestDto = mapper.Map<UpdateProjectRequestCommand>(project);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task AddProjectRequest(UpdateProjectViewModel project)
        {
            var requestDto = mapper.Map<AddProjectRequestCommand>(project);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task ProcessDegreeRequest(List<UpdateDegreeViewModel> degrees)
        {
            foreach (var degree in degrees)
            {
                if ((degree.DegreeId == null) && (degree.Name != null))
                {
                    await AddDegreeRequest(degree);
                }

                else if ((degree.DegreeId != null) && (degree.Name != null))
                {
                    await UpdateDegreeRequest(degree);
                }

                else if ((degree.DegreeId != null) && (degree.Name == null))
                {
                    await DeleteDegreeRequest(degree);
                }
            }
        }

        private async Task DeleteDegreeRequest(UpdateDegreeViewModel degree)
        {
            var requestDto = mapper.Map<DeleteDegreeRequestCommand>(degree);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task UpdateDegreeRequest(UpdateDegreeViewModel degree)
        {
            var requestDto = mapper.Map<UpdateDegreeRequestCommand>(degree);

            var response = await mediator.Send(requestDto);

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        private async Task AddDegreeRequest(UpdateDegreeViewModel degree)
        {
            var requestDto = mapper.Map<AddDegreeRequestCommand>(degree);

            var response = await mediator.Send(requestDto); 

            ResponsesList.Add(mapper.Map<Response>(response));
        }

        
    }
}
