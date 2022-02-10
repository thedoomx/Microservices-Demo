namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;

    internal class EmployeeSurveyConfiguration : IEntityTypeConfiguration<EmployeeSurvey>
    {
        public void Configure(EntityTypeBuilder<EmployeeSurvey> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.EmployeeId)
                .IsRequired();

            builder
               .HasOne(c => c.Survey)
               .WithMany()
               .HasForeignKey("SurveyId")
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.IsSubmitted)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .HasMany(d => d.EmployeeSurveyItems)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("employeeSurveyItems");
        }
    }
}
