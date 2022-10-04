using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public class TelegramNotificator : INotificator
    {
        TelegramBotClient _botClient;

        public TelegramNotificator(IConfiguration config)
        {
            var token = config["telegram_Token"];
            _botClient = new TelegramBotClient(token);
            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            //_botClient.SetMyCommandsAsync()
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            return Task.CompletedTask;
        }

        private Task HandleUpdateAsync(ITelegramBotClient arg1, Update arg2, CancellationToken arg3)
        {
            return Task.CompletedTask;
        }

        public void SendNotification(NotificationData data)
        {
           // _botClient.SendTextMessageAsync();
        }

    }
}
