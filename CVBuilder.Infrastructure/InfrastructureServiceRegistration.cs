using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.CVRequest;
using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateCVRequest;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Contracts.UploadEmailToQueue;
using CVBuilder.Application.Models.Authentication;
using CVBuilder.Application.Models.Azure;
using CVBuilder.Infrastructure.Data;
using CVBuilder.Infrastructure.Repositories;
using CVBuilder.Infrastructure.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.Configure<AzureSettings>(configuration.GetSection("AzureSettings"));


            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<IDegreeRepository, DegreeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();
            services.AddScoped<IResourceRequestRepository, ResourceRequestRepository>();
            services.AddScoped<IDegreeUpdateRepository, DegreeUpdateRepository>();
            services.AddScoped<IPersonalDetailsUpdateRepository, PersonalDetailsUpdateRepository>();
            services.AddScoped<IProjectUpdateRepository, ProjectUpdateRepository>();
            services.AddScoped<ISkillUpdateRepository, SkillUpdateRepository>();
            services.AddScoped<IWorkExperienceUpdateRepository, WorkExperienceUpdateRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyDetailsRepository, CompanyDetailsRepository>();
            services.AddScoped<ICVRequestRepository, CVRequestRepository>();
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            
            services.AddTransient<IUploadEmailToQueueService, UploadEmailToQueueService>();

            services.AddScoped<IUpdateResourceService, UpdateResourceService>();
            services.AddScoped<IUpdateDegreeService, UpdateDegreeService>();
            services.AddScoped<IUpdatePersonalDetailsService, UpdatePersonalDetailsService>();
            services.AddScoped<IUpdateProjectService, UpdateProjectService>();
            services.AddScoped<IUpdateSkillService, UpdateSkillService>();
            services.AddScoped<IUpdateWorkExperienceService, UpdateWorkExperienceService>();
            
            services.AddScoped<IUpdateCVRequestService, UpdateCVRequestService>();

            services.AddScoped<ICVRequestService, CVRequestService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });


            return services;
        }
    }
}
