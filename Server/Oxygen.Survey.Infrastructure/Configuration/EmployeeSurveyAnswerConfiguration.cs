namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    //using static Domain.Models.ModelConstants.UserSurveyItem;

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
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
