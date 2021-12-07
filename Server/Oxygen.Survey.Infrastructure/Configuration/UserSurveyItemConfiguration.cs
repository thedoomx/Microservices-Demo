namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    //using static Domain.Models.ModelConstants.UserSurveyItem;

    internal class UserSurveyItemConfiguration : IEntityTypeConfiguration<UserSurveyItem>
    {
        public void Configure(EntityTypeBuilder<UserSurveyItem> builder)
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
                .HasOne(c => c.QuestionItem)
                .WithMany()
                .HasForeignKey("QuestionItemId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
