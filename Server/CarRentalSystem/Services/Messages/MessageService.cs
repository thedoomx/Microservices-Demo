namespace CarRentalSystem.Services.Messages
{
    using System;
    using System.Threading.Tasks;
    using CarRentalSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class MessageService : IMessageService
    {
        private MessageDbContext data;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageService(IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }

        //public MessageService(DbContext data) 
        //    => this.data = data as MessageDbContext 
        //        ?? throw new InvalidOperationException($"Messages can only be used with a {nameof(MessageDbContext)}.");

        public async Task<bool> IsDuplicated(
            object messageData, 
            string propertyFilter,
            object identifier)
        {
            var messageType = messageData.GetType();

            if (data == null)
            {
                using var scope = _serviceScopeFactory.CreateScope();

                this.data = scope.ServiceProvider.GetService<DbContext>() as MessageDbContext;
            }

            return await this.data
                .Messages
                .FromSqlRaw($"SELECT * FROM Messages WHERE Type = '{messageType.AssemblyQualifiedName}' AND JSON_VALUE(serializedData, '$.{propertyFilter}') = {identifier}")
                .AnyAsync();
        }
    }
}
