namespace Ogyxen.Application.Common
{
    using Microsoft.Extensions.Configuration;
    using Oxygen.Application.Common;

    public static class MessageQueueSettingsHelper
    {
        public static MessageQueueSettings GetMessageQueueSettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(MessageQueueSettings));

            return new MessageQueueSettings(
                settings.GetValue<string>(nameof(MessageQueueSettings.Host)),
                settings.GetValue<string>(nameof(MessageQueueSettings.UserName)),
                settings.GetValue<string>(nameof(MessageQueueSettings.Password)));
        }
    }
}
