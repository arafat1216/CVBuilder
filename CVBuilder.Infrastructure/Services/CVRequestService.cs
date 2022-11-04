using AutoMapper;
using CVBuilder.Application.Contracts.CVRequest;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Dtos.Employee;
using CVBuilder.Application.Exceptions;
using CVBuilder.Application.ViewModels.Company;
using CVBuilder.Domain.Entities;
using CVBuilder.Domain.Others;

namespace CVBuilder.Infrastructure.Services
{
    public class CVRequestService : ICVRequestService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly ICVRequestRepository cVRequestRepository;
        private readonly ICompanyRepository companyRepository;

        public CVRequestService(IEmployeeRepository employeeRepository, IMapper mapper, ICVRequestRepository cVRequestRepository, ICompanyRepository companyRepository)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.cVRequestRepository = cVRequestRepository;
            this.companyRepository = companyRepository;
        }

        public async Task<List<EmployeeDetailsDto>> RequestCV(CVRequestViewModel cvRequestViewModel)
        {
            // check if maximum cv request limit exceeded
            var currentDate = DateTime.Now.ToShortDateString();

            var cvRequest = await cVRequestRepository.GetCVRequest(cvRequestViewModel.CompanyId, currentDate);

            // check if todays request count is empty
            if (cvRequest == null)
            {
                await AddRequest(cvRequestViewModel, currentDate);
            }
            else
            {
                var maximumCVRequests = await GetCVRequestLimit(cvRequest.CompanyId);

                if (maximumCVRequests <= cvRequest.RequestsAmount)
                    throw new BadRequestException("Maximum CV Request Limit Exceeded");

                await UpdateRequest(cvRequest);
            }

            var employees = await employeeRepository.GetAllEmployeesCVAsync(cvRequestViewModel);

            return mapper.Map<List<EmployeeDetailsDto>>(employees);
        }

        private async Task<int> GetCVRequestLimit(Guid companyId)
        {
            var company = await companyRepository.GetCompanyByIdAsync(companyId);

            return MaximumCVRequests.GetCVRequestLimit(company.SubscriptionType);
        }

        private async Task UpdateRequest(CVRequest cvRequest)
        {
            
            cvRequest.RequestsAmount = cvRequest.RequestsAmount + 1;

            // update today's request
            await cVRequestRepository.UpdateAsync(cvRequest);
        }

        private async Task AddRequest(CVRequestViewModel cvRequestViewModel, string currentDate)
        {
            var cvRequestToAdd = new Domain.Entities.CVRequest()
            {
                CompanyId = cvRequestViewModel.CompanyId,
                Date = currentDate,
                RequestsAmount = 1
            };
            // store today's request

            await cVRequestRepository.AddAsync(cvRequestToAdd);
        }
    }
}
