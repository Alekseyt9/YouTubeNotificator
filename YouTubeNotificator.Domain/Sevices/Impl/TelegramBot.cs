
using Microsoft.Extensions.Configuration;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using YouTubeNotificator.Domain.Model;
using Telegram.Bot.Types;

namespace YouTubeNotificator.Domain.Sevices.Impl
{
    internal class TelegramBot : ITelegramBot
    {
        public event EventHandler<TelegramMessageEventArgs>? ReceiveMessage;

        TelegramBotClient _botClient;

        public TelegramBot(IConfiguration config)
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
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            if (ReceiveMessage != null)
            {
                ReceiveMessage(this, new TelegramMessageEventArgs()
                {
                    ChannelId = chatId,
                    Message = messageText
                });
            }
        }


        public void SendMessage(long channelId, string msg)
        {
            throw new NotImplementedException();
        }

    }
}
