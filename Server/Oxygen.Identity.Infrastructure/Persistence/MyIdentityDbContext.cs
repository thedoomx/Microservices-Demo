namespace Oxygen.Identity.Infrastructure.Persistence
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Domain.Common.Models;
    using Oxygen.Infrastructure.Common.Events;
    using Oxygen.Infrastructure.Common.Persistence;
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
            OnBeforeSaveChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();

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
                    var userCreateMessage = new Message(entry);
                    this.Messages.Add(userCreateMessage);
                }
            }
        }
    }

    //internal class MyIdentityDbContext : IdentityDbContext<User, Role, string>
    //{
    //    private readonly IEventDispatcher eventDispatcher;
    //    private readonly Stack<object> savesChangesTracker;

    //    public MyIdentityDbContext(
    //        DbContextOptions<MyIdentityDbContext> options,
    //        IEventDispatcher eventDispatcher)
    //        : base(options)
    //    {
    //        this.eventDispatcher = eventDispatcher;

    //        this.savesChangesTracker = new Stack<object>();
    //    }


    //    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //    {
    //        this.savesChangesTracker.Push(new object());

    //        var entities = this.ChangeTracker
    //            .Entries<IEntity>()
    //            .Select(e => e.Entity)
    //            .Where(e => e.Events.Any())
    //            .ToArray();

    //        foreach (var entity in entities)
    //        {
    //            var events = entity.Events.ToArray();

    //            entity.ClearEvents();

    //            foreach (var domainEvent in events)
    //            {
    //                await this.eventDispatcher.Dispatch(domainEvent);
    //            }
    //        }

    //        this.savesChangesTracker.Pop();

    //        if (!this.savesChangesTracker.Any())
    //        {
    //            return await base.SaveChangesAsync(cancellationToken);
    //        }

    //        return 0;
    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    //        base.OnModelCreating(builder);
    //    }
    //}
}
