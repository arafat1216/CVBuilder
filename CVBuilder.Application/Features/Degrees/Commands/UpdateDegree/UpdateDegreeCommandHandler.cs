using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.UpdateDegree
{
    public class UpdateDegreeCommandHandler : IRequestHandler<UpdateDegreeCommand>
    {
        private readonly IDegreeRepository repository;
        private readonly IMapper mapper;

        public UpdateDegreeCommandHandler(IDegreeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task<Unit> Handle(UpdateDegreeCommand request, CancellationToken cancellationToken)
        {
            #region check if degree exists

            var degreeExists = await repository.ExistsAsync(request.EmployeeId, request.DegreeId);

            if (!degreeExists)
                throw new Exceptions.NotFoundException(nameof(Degree), request.DegreeId);

            #endregion

            #region validate incoming request

            var validator = new UpdateDegreeCommandValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            #endregion

            #region mapping incoming request to degree entity

            var degreeToUpdate = mapper.Map<Degree>(request);

            #endregion

            await repository.UpdateAsync(degreeToUpdate);

            return Unit.Value;
        }
    }
}
