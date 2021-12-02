namespace Oxygen.Survey.Infrastructure.Persistence
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Infrastructure.Common.Events;
    using Domain.Models;
    using Oxygen.Domain.Common.Models;
    using Oxygen.Infrastructure.Common.Persistence;
    using Oxygen.Survey.Infrastructure.Persistence.Models;

    internal class SurveyDbContext : MessageDbContext,
        ISurveyDbContext
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly Stack<object> savesChangesTracker;

        public SurveyDbContext(
            DbContextOptions<SurveyDbContext> options,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            this.eventDispatcher = eventDispatcher;

            this.savesChangesTracker = new Stack<object>();
        }

        public DbSet<Survey> Surveys { get; set; } = default!;

        public DbSet<SurveyType> SurveyTypes { get; set; } = default!;
        
        public DbSet<Question> Questions { get; set; } = default!;

        public DbSet<QuestionType> QuestionTypes { get; set; } = default!;

        public DbSet<QuestionItem> QuestionItems { get; set; } = default!;

        public DbSet<UserSurveyData> UserSurveys { get; set; } = default!;

        public DbSet<UserSurveyItem> UserSurveyItems { get; set; } = default!;

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.savesChangesTracker.Push(new object());

            var entities = this.ChangeTracker
                .Entries<IEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.Events.ToArray();

                entity.ClearEvents();

                foreach (var domainEvent in events)
                {
                    await this.eventDispatcher.Dispatch(domainEvent);
                }
            }

            this.savesChangesTracker.Pop();

            if (!this.savesChangesTracker.Any())
            {
                return await base.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
