namespace Oxygen.Identity.Infrastructure.Persistence
{
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Messages.Users;
    using Oxygen.Infrastructure.Common.Persistence.Configuration;
    using Oxygen.Infrastructure.Common.Persistence.Models;

    internal class MyIdentityDbContext : IdentityDbContext<User, Role, string>
    {
        public MyIdentityDbContext(
           DbContextOptions<MyIdentityDbContext> options)
           : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            OnBeforeSaveChanges();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var changedEntries = ChangeTracker.Entries().ToList();
            foreach (var entry in changedEntries)
            {
                if (entry.Entity is User &&
                    entry.State == EntityState.Added)
                {
                    var userEntity = (User)entry.Entity;
                    var userCreatedMessage = 
                        new UserCreatedMessage(userEntity.Id, userEntity.FirstName, userEntity.SurName,
                        userEntity.LastName, userEntity.Department.Value, userEntity.JobTitle.Value, userEntity.Office.Value);

                    var message = new Message(userCreatedMessage);
                    this.Messages.Add(message);
                }
            }
        }
    }
}
