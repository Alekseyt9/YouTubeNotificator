
using YouTubeNotificator.Domain.Commands;

namespace YouTubeNotificator.Domain.Sevices.Implementation
{
    public class TelegramCommandFactory : ITelegramCommandFactory
    {
        public object Create(CommandInfo cmdInfo)
        {
            switch (cmdInfo.Kind)
            {
                case TelegramCommandKind.Start:
                    return new StartCommand();

                case TelegramCommandKind.List:
                    return new ListChannelsCommand();

                case TelegramCommandKind.Remove:
                    return new DelChannelCommand();

                case TelegramCommandKind.Add:
                    return new AddChannelCommand();
                    
                default: throw new ArgumentException($"Wrong command {cmdInfo.Kind}");
            }
        }
    }
}
