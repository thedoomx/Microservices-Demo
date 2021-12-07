namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    //using static Domain.Models.ModelConstants.UserSurvey;

    internal class UserSurveyConfiguration : IEntityTypeConfiguration<UserSurvey>
    {
        public void Configure(EntityTypeBuilder<UserSurvey> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.UserId)
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
                .HasMany(d => d.UserSurveyItems)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("userSurveyItems");
        }
    }
}
