namespace Oxygen.Identity.Infrastructure.Configuration
{
    using Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Ignore(x => x.FirstName);
            builder.Ignore(x => x.SurName);
            builder.Ignore(x => x.LastName);
            builder.Ignore(x => x.Department);
            builder.Ignore(x => x.JobTitle);
            builder.Ignore(x => x.Office);
        }
    }
}
