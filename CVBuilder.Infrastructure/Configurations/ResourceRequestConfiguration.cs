using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class ResourceRequestConfiguration : IEntityTypeConfiguration<ResourceRequest>
    {
        public void Configure(EntityTypeBuilder<ResourceRequest> builder)
        {
            // Relationships

            builder.HasKey(r => r.RequestId);

            builder.HasOne(r => r.PersonalDetailsUpdateRequest).WithOne()
                .HasForeignKey<PersonalDetailsUpdateRequest>(p => p.RequestId);

            builder.HasKey(r => r.RequestId);

            builder.HasOne(r => r.DegreeUpdateRequest).WithOne()
                .HasForeignKey<DegreeUpdateRequest>(d => d.RequestId);

            builder.HasOne(r => r.PersonalDetailsUpdateRequest).WithOne()
                .HasForeignKey<PersonalDetailsUpdateRequest>(p => p.RequestId);

            builder.HasOne(r => r.ProjectUpdateRequest).WithOne()
                .HasForeignKey<ProjectUpdateRequest>(p => p.RequestId);

            builder.HasOne(r => r.SkillUpdateRequest).WithOne()
                .HasForeignKey<SkillUpdateRequest>(s => s.RequestId);

            builder.HasOne(r => r.WorkExperienceUpdateRequest).WithOne()
                .HasForeignKey<WorkExperienceUpdateRequest>(w => w.RequestId);


        }
    }
}
