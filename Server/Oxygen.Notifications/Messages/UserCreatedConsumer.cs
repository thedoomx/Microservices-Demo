namespace Oxygen.Notifications.Messages
{
    using System.Threading.Tasks;
    using Hub;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using Oxygen.Infrastructure.Common.Messages.Survey;

    using static Constants;

    public class UserCreatedConsumer : IConsumer<UserSurveyCreatedMessage>
    {
        private readonly IHubContext<NotificationsHub> hub;

        public UserCreatedConsumer(IHubContext<NotificationsHub> hub)
            => this.hub = hub;

        public async Task Consume(ConsumeContext<UserSurveyCreatedMessage> context)
            => await this.hub
                .Clients
                .Groups(AuthenticatedUsersGroup)
                .SendAsync(ReceiveNotificationEndpoint, context.Message);
    }
}
