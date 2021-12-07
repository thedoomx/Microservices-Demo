namespace Oxygen.Notifications.Messages
{
    using System.Threading.Tasks;
    using Hub;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using Oxygen.Infrastructure.Common.Messages.Users;

    using static Constants;

    public class UserSurveyCreatedConsumer : IConsumer<UserCreatedMessage>
    {
        private readonly IHubContext<NotificationsHub> hub;

        public UserSurveyCreatedConsumer(IHubContext<NotificationsHub> hub)
            => this.hub = hub;

        public async Task Consume(ConsumeContext<UserCreatedMessage> context)
            => await this.hub
                .Clients
                .User(context.Message.UserId)
                .SendAsync(ReceiveNotificationEndpoint, context.Message);
    }
}
