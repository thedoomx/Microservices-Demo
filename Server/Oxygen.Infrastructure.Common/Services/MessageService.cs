namespace Oxygen.Infrastructure.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Oxygen.Infrastructure.Common.Persistence;

    public class MessageService : IMessageService
    {
        private readonly MessageDbContext data;

        //public MessageService(IServiceScopeFactory serviceScopeFactory)
        //{
        //    using var scope = serviceScopeFactory.CreateScope();

        //    this.data = scope.ServiceProvider.GetService<DbContext>() as MessageDbContext;
        //}

        public MessageService(DbContext data)
            => this.data = data as MessageDbContext
                ?? throw new InvalidOperationException($"Messages can only be used with a {nameof(MessageDbContext)}.");

        public async Task<bool> IsDuplicated(
            object messageData,
            string propertyFilter,
            object identifier)
        {
            var messageType = messageData.GetType();

            return await this.data
                    .Messages
                    .FromSqlRaw($"SELECT * FROM Messages WHERE Type = '{messageType.AssemblyQualifiedName}' AND JSON_VALUE(serializedData, '$.{propertyFilter}') = '{identifier}'")
                    .AnyAsync();
        }
    }
}
