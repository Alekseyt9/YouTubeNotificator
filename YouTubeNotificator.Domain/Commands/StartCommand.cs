

using MediatR;

namespace YouTubeNotificator.Domain.Commands
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public class StartCommand : TelegramCommandBase, IRequest
    {

    }
}
