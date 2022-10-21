using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceHelper;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Enums;
using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest.UpdateRequest
{
    public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand>
    {
        private readonly IResourceRequestRepository resourceRequestRepository;
        private readonly IApplicationUser applicationUser;
        private readonly IMediator mediator;
        private readonly IUpdateResourceHelperService updateResourceHelperService;

        public UpdateRequestCommandHandler(IResourceRequestRepository resourceRequestRepository, IApplicationUser applicationUser, IMediator mediator, IUpdateResourceHelperService updateResourceHelperService)
        {
            this.resourceRequestRepository = resourceRequestRepository;
            this.applicationUser = applicationUser;
            this.mediator = mediator;
            this.updateResourceHelperService = updateResourceHelperService;
        }
        public async Task<Unit> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var requestDetails = await resourceRequestRepository.GetResourceRequestByIdAsync(request.RequestId);

            if (requestDetails == null)
                throw new Exceptions.NotFoundException(nameof(ResourceRequest), request.RequestId);

            requestDetails.ReviewedBy = applicationUser.GetUserId();


            if (!request.IsApproved)
            {
                requestDetails.Status = Status.Rejected.ToString();

                await resourceRequestRepository.UpdateAsync(requestDetails);

                return Unit.Value;
            }

            await UpdateResource(requestDetails);

            requestDetails.Status = Status.Approved.ToString();

            await resourceRequestRepository.UpdateAsync(requestDetails);

            return Unit.Value;

        }

        private async Task UpdateResource(ResourceRequest requestDetails)
        {

            var resourceDetails = await resourceRequestRepository.GetResourceRequestDetailsAsync(requestDetails.RequestId);

            if (requestDetails.RequestType == RequestType.Add.ToString() && requestDetails.ResourceType == ResourceType.Degree.ToString())
            {
                await updateResourceHelperService.AddDegree(requestDetails, resourceDetails.DegreeUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Modify.ToString() && requestDetails.ResourceType == ResourceType.Degree.ToString())
            {
                await updateResourceHelperService.UpdateDegree(requestDetails, resourceDetails.DegreeUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Modify.ToString() && requestDetails.ResourceType == ResourceType.PersonalDetails.ToString())
            {
                await updateResourceHelperService.UpdatePersonalDetails(requestDetails,resourceDetails.PersonalDetailsUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Add.ToString() && requestDetails.ResourceType == ResourceType.Project.ToString())
            {
                await updateResourceHelperService.AddProject(requestDetails, resourceDetails.ProjectUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Modify.ToString() && requestDetails.ResourceType == ResourceType.Project.ToString())
            {
                await updateResourceHelperService.UpdateProject(requestDetails, resourceDetails.ProjectUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Add.ToString() && requestDetails.ResourceType == ResourceType.Skill.ToString())
            {
                await updateResourceHelperService.AddSkill(requestDetails, resourceDetails.SkillUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Modify.ToString() && requestDetails.ResourceType == ResourceType.Skill.ToString())
            {
                await updateResourceHelperService.UpdateSkill(requestDetails, resourceDetails.SkillUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Add.ToString() && requestDetails.ResourceType == ResourceType.WorkExperience.ToString())
            {
                await updateResourceHelperService.AddWorkExperience(requestDetails, resourceDetails.WorkExperienceUpdateRequest);
            }

            else if (requestDetails.RequestType == RequestType.Modify.ToString() && requestDetails.ResourceType == ResourceType.WorkExperience.ToString())
            {
                await updateResourceHelperService.UpdateWorkExperience(requestDetails, resourceDetails.WorkExperienceUpdateRequest);
            }



        }
    }
}
