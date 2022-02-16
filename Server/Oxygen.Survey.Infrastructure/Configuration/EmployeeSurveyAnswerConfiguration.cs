namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.EmployeeSurveyAnswer;

    internal class EmployeeSurveyAnswerConfiguration : IEntityTypeConfiguration<EmployeeSurveyAnswer>
    {
        public void Configure(EntityTypeBuilder<EmployeeSurveyAnswer> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasOne(c => c.Question)
                .WithMany()
                .HasForeignKey("QuestionId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.QuestionAnswer)
                .WithMany()
                .HasForeignKey("QuestionAnswerId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.TextValue)
                .IsRequired()
                .HasMaxLength(MaxTextValueLength);

            builder
                .Property(c => c.BoolValue)
                .HasDefaultValue(null);
        }
    }
}
