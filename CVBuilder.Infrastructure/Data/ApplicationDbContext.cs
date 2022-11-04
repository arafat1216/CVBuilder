using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ResourceRequest> ResourceRequests { get; set; }
        public DbSet<DegreeUpdateRequest> DegreeUpdateRequests { get; set; }
        public DbSet<PersonalDetailsUpdateRequest> PersonalDetailsUpdateRequests { get; set; }
        public DbSet<ProjectUpdateRequest> ProjectUpdateRequests { get; set; }
        public DbSet<SkillUpdateRequest> SkillUpdateRequests { get; set; }
        public DbSet<WorkExperienceUpdateRequest> WorkExperienceUpdateRequests { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyDetails> CompanyDetails { get; set; }
        public DbSet<CVRequest> cVRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
