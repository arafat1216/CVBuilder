using CVBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(50);

            builder.Property(e => e.Email).IsRequired();

            builder.Property(e => e.Password).IsRequired();

            builder.Property(e => e.PhoneNo).IsRequired();


            // Relationships

            builder.HasMany(s => s.Skills).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);

            builder.HasMany(d => d.Degrees).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);

            builder.HasMany(w => w.WorkExperiences).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);

            builder.HasMany(p => p.Projects).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);

        }
    }
}
