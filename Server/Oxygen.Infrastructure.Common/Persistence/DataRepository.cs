﻿namespace Oxygen.Infrastructure.Common.Persistence
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Domain.Common;
	using Oxygen.Infrastructure.Common.Persistence.Models;
	using Oxygen.Infrastructure.Common.Services;

	public abstract class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>
		where TDbContext : IDbContext
		where TEntity : class, IAggregateRoot
	{
		protected DataRepository(TDbContext db, IPublisher publisher)
		{
			this.Data = db;
			this.Publisher = publisher;
		}

		protected TDbContext Data { get; }

		protected IPublisher Publisher { get; }

		protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

		public async Task Save(
			TEntity entity,
			CancellationToken cancellationToken = default,
			params object[] messages)
		{
			this.Data.Update(entity);

			await this.Save(cancellationToken, messages);
		}

		public async Task Save(
		   IEnumerable<TEntity> entities,
		   CancellationToken cancellationToken = default,
		   params object[] messages)
		{
			foreach (var entity in entities)
			{
				this.Data.Update(entity);
			}

			await this.Save(cancellationToken, messages);
		}

		private async Task Save(CancellationToken cancellationToken = default, params object[] messages)
		{
			var dataMessages = messages
				.ToDictionary(data => data, data => new Message(data));

			if (this.Data is MessageDbContext)
			{
				foreach (var (_, message) in dataMessages)
				{
					this.Data.Update(message);
				}
			}

			await this.Data.SaveChangesAsync(cancellationToken);

			if (this.Data is MessageDbContext)
			{
				foreach (var (data, message) in dataMessages)
				{
					await this.Publisher.Publish(data);

					message.MarkAsPublished();

					await this.Data.SaveChangesAsync();
				}
			}
		}
	}
}
