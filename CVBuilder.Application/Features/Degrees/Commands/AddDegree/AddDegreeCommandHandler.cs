﻿using AutoMapper;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Domain.Entities;
using MediatR;

namespace CVBuilder.Application.Features.Degrees.Commands.AddDegree
{
    public class AddDegreeCommandHandler : IRequestHandler<AddDegreeCommand, AddDegreeCommandResponse>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly IMapper mapper;

        public AddDegreeCommandHandler(IEmployeeRepository employeeRepository, IDegreeRepository degreeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.degreeRepository = degreeRepository;
            this.mapper = mapper;
        }
        public async Task<AddDegreeCommandResponse> Handle(AddDegreeCommand request, CancellationToken cancellationToken)
        {
            #region check if employee exists

            var employeeExists = await employeeRepository.EmployeeExistsAsync(request.EmployeeId);

            if (!employeeExists)
                throw new Exceptions.NotFoundException(nameof(Employee), request.EmployeeId);

            #endregion

            #region validate incoming request

            var validator = new AddDegreeCommandValidator();

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult);

            #endregion

            #region mapping incoming request to degree entity

            var degreeToAdd = mapper.Map<Degree>(request);

            #endregion

            var response = await degreeRepository.AddAsync(degreeToAdd);

            return mapper.Map<AddDegreeCommandResponse>(response);
        }
    }
}