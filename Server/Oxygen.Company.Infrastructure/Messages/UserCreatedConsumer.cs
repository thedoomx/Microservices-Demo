namespace Oxygen.Company.Infrastructure.Messages
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Oxygen.Company.Infrastructure.Persistence;
    using Oxygen.Infrastructure.Common.Messages.Users;
    using Oxygen.Infrastructure.Common.Persistence.Models;
    using Oxygen.Infrastructure.Common.Services;

    internal class UserCreatedConsumer : IConsumer<UserCreatedMessage>
    {
        private readonly CompanyDbContext data;
        private readonly IMessageService messages;

        public UserCreatedConsumer(
            CompanyDbContext data,
            IMessageService messages)
        {
            this.data = data;
            this.messages = messages;
        }

        public async Task Consume(ConsumeContext<UserCreatedMessage> context)
        {
            var message = context.Message;

            var isDuplicated = await this.messages.IsDuplicated(
                message,
                nameof(UserCreatedMessage.UserId),
                message.UserId);

            if (isDuplicated)
            {
                return;
            }

            //var statistics = await this.data.Statistics.SingleOrDefaultAsync();
            //statistics.TotalCarAds++;

            var dataMessage = new Message(message);

            dataMessage.MarkAsPublished();

            this.data.Messages.Add(dataMessage);

            await this.data.SaveChangesAsync();
        }
    }
}
