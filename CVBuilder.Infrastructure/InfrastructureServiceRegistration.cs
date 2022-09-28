using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Infrastructure.Data;
using CVBuilder.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<IDegreeRepository, DegreeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();

            return services;
        }
    }
}
