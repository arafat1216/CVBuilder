﻿using MediatR;

namespace CVBuilder.Application.Features.UpdatePassword.Commands
{
    public class UpdatePasswordCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
