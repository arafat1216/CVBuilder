using CVBuilder.Application.Contracts.Authentication;
using CVBuilder.Application.Contracts.PdfGenerator;
using CVBuilder.Application.Contracts.Persistence;
using CVBuilder.Application.Contracts.UpdateResourceHelper;
using CVBuilder.Application.Contracts.UpdateResourceManager;
using CVBuilder.Application.Models.Authentication;
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
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            
            services.AddScoped<ITemplateGeneratorService, TemplateGeneratorService>();
            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            
            services.AddScoped<IUpdateResourceService, UpdateResourceService>();
            services.AddScoped<IUpdateDegreeService, UpdateDegreeService>();
            services.AddScoped<IUpdatePersonalDetailsService, UpdatePersonalDetailsService>();
            services.AddScoped<IUpdateProjectService, UpdateProjectService>();
            services.AddScoped<IUpdateSkillService, UpdateSkillService>();
            services.AddScoped<IUpdateWorkExperienceService, UpdateWorkExperienceService>();

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
