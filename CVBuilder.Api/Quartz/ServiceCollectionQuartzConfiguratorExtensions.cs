using Quartz;

namespace CVBuilder.Api.Quartz
{
    public static class ServiceCollectionQuartzConfiguratorExtensions
    {
        public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator configurator, IConfiguration configuration) where T : IJob
        {
            string jobName = typeof(T).Name;

            var configKey = $"Quartz:{jobName}";

            var cronSchedule = configuration[configKey];

            if (string.IsNullOrEmpty(cronSchedule))
            {
                throw new Exception($"No Quartz.NET Cron Schedule found for job in configuration at {configKey}");
            }

            var jobKey = new JobKey(jobName);

            configurator.AddJob<T>(opts => opts.WithIdentity(jobKey));

            configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(cronSchedule));
        }
    }
}
