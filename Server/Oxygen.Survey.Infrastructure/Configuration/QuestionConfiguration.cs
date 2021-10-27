namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    using static Domain.Models.ModelConstants.Question;

    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
                .Property(c => c.IsRequired)
                .IsRequired()
                .HasDefaultValue(false);

            builder
                .HasOne(c => c.QuestionType)
                .WithMany()
                .HasForeignKey("QuestionTypeId")
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasMany(d => d.QuestionItems)
               .WithOne()
               .Metadata
               .PrincipalToDependent
               .SetField("questionItems");
        }
    }
}
