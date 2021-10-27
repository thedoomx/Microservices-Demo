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
                .Property(c => c.QuestionId)
                .IsRequired();

            builder
                .Property(c => c.QuestionItemId)
                .IsRequired();
        }
    }
}
