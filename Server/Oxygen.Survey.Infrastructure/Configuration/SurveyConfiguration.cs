namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    using static Domain.Models.ModelConstants.Survey;

    internal class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(c => c.Summary)
                .IsRequired()
                .HasMaxLength(MaxSummaryLength);

            builder
                .HasOne(c => c.SurveyType)
                .WithMany()
                .HasForeignKey("SurveyTypeId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
