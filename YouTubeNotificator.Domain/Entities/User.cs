namespace YouTubeNotificator.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public long TelegramId { get; set; }

        public ICollection<UserChannel> Channels { get; set; }
    }
}
