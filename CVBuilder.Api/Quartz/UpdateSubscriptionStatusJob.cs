using CVBuilder.Application.Features.Company.Commands.UpdateSubscriptionStatus;
using CVBuilder.Application.Features.Company.Queries.GetCompaniesList;
using CVBuilder.Application.Features.Company.Queries.GetCompanyDetails;
using CVBuilder.Domain.Enums;
using MediatR;
using Quartz;

namespace CVBuilder.Api.Quartz
{
    public class UpdateSubscriptionStatusJob : IJob
    {
        private readonly ILogger<UpdateSubscriptionStatusJob> logger;
        private readonly IServiceProvider serviceProvider;

        public UpdateSubscriptionStatusJob(ILogger<UpdateSubscriptionStatusJob> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();

                var requestDto = new GetCompaniesListQuery();

                var companiesList = await mediator.Send(requestDto);

                foreach (var company in companiesList)
                {
                    var currentDate = DateTime.Now;
                    var differenceBetweenDates = currentDate - company.SubscriptionPurchasedDate;

                    if (differenceBetweenDates.TotalDays >= 30)
                    {
                        var updateSubscriptionStatusDto = new UpdateSubscriptionStatusCommand()
                        {
                            CompanyId = company.CompanyId,
                            SubscriptionStatus = SubscriptionStatus.Expired
                        };

                        await mediator.Send(updateSubscriptionStatusDto);
                    }
                }

                
            }

            
        }
    }
}
