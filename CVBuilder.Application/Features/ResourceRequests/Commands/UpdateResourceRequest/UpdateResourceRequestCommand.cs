﻿using MediatR;

namespace CVBuilder.Application.Features.ResourceRequests.Commands.UpdateResourceRequest
{
    public class UpdateResourceRequestCommand : IRequest
    {
        public int RequestId { get; set; }
        public bool IsApproved { get; set; }
    }
}
