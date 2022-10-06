﻿
using MediatR;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Domain.Commands.Handlers
{
    /// <summary>
    /// Создает пользователя в базе и записывает ChannelId
    /// </summary>
    internal class StartCommandHandler : AsyncRequestHandler<StartCommand>
    {
        private ITelegramBot _telegramBot;

        public StartCommandHandler(ITelegramBot telegramBot)
        {
            _telegramBot = telegramBot ?? throw new ArgumentNullException(nameof(telegramBot));
        }

        protected override Task Handle(StartCommand request, CancellationToken cancellationToken)
        {
            _telegramBot.SendMessage(
                request.Context.TelegramChannelId, "cmd:start");
            return Task.CompletedTask;
        }
    }
}
