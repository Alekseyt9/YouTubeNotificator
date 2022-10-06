

using User = YouTubeNotificator.Domain.Entities.User;

namespace YouTubeNotificator.Domain.Sevices
{
    public interface INotificationTaskManager
    {
        /// <summary>
        /// Начальная загрузка, запуск задач
        /// </summary>
        Task Start();

        /// <summary>
        /// Создание задачи при добавлении пользователя
        /// </summary>
        Task AddUserTask(User user);

    }
}
