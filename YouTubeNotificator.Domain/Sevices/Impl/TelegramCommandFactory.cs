
using YouTubeNotificator.Domain.Commands;
using YouTubeNotificator.Domain.Model;

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
                    return new DelChannelCommand()
                    {
                        ChannelUrl = cmdInfo.Params.First()
                    };

                case TelegramCommandKind.Add:
                    return new AddChannelCommand()
                    {
                        ChannelUrl = cmdInfo.Params.First()
                    };
                    
                default: throw new ArgumentException($"Wrong command {cmdInfo.Kind}");
            }
        }
    }
}
