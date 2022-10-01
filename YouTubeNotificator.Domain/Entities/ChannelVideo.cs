namespace YouTubeNotificator.Domain.Entities
{
    public class ChannelVideo
    {
        public Guid Id { get; set; }

        public Guid ChannelId { get; set; }

        public DateTime Date { get; set; }

    }
}
