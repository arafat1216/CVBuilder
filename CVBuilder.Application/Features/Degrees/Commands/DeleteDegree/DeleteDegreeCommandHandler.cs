﻿using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.DeleteDegree
{
    public class DeleteDegreeCommandHandler : IRequestHandler<DeleteDegreeCommand>
    {
        private readonly IDegreeRepository repository;

        public DeleteDegreeCommandHandler(IDegreeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Unit> Handle(DeleteDegreeCommand request, CancellationToken cancellationToken)
        {
            // fetch degree  

            var degreeToDelete = await GetDegreeToDelete(request.EmployeeId, request.DegreeId);

            
            if (degreeToDelete == null)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);


            if (request.SoftDelete)
            {
                degreeToDelete.IsDeleted = true;

                await repository.UpdateAsync(degreeToDelete);

                return Unit.Value;
            }


            await repository.DeleteAsync(degreeToDelete);

            return Unit.Value;
        }

        private async Task<Degree?> GetDegreeToDelete(Guid employeeId, int degreeId)
        {
            return await repository.GetDegreeByIdAsync(employeeId, degreeId);
        }
    }
}
