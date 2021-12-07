namespace Oxygen.Company.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    using static Domain.Models.ModelConstants.Employee;

    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(MaxFirstNameLength);

            builder
                .Property(c => c.SurName)
                .IsRequired()
                .HasMaxLength(MaxSurNameLength);

            builder
                .Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(MaxLastNameLength);

            builder
               .HasOne(c => c.Department)
               .WithMany()
               .HasForeignKey("DepartmentId")
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(c => c.Office)
               .WithMany()
               .HasForeignKey("OfficeId")
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(c => c.JobTitle)
               .WithMany()
               .HasForeignKey("JobTitleId")
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
