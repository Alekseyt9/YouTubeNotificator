namespace YouTubeNotificator.Domain.Sevices
{
    public interface ITelegramCommandFactory
    {
        object Create(CommandInfo cmdInfo);
    }
}
