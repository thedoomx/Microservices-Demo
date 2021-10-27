namespace Oxygen.Survey.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Domain.Models.ModelConstants.Common;
    using static Domain.Models.ModelConstants.QuestionType;

    internal class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(MaxTypeLength);
        }
    }
}
