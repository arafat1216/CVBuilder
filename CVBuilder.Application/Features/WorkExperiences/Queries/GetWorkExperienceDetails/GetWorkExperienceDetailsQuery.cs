﻿using CVBuilder.Application.ViewModels.WorkExperience;
using MediatR;

namespace CVBuilder.Application.Features.WorkExperiences.Queries.GetWorkExperienceDetails
{
    public class GetWorkExperienceDetailsQuery : IRequest<WorkExperienceViewModel>
    {
        public Guid EmployeeId { get; set; }
        public int WorkExperienceId { get; set; }
    }
}
