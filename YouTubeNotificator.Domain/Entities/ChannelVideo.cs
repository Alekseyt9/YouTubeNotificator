namespace YouTubeNotificator.Domain.Entities
{
    public class ChannelVideo
    {
        public Guid Id { get; set; }

        public Guid ChannelId { get; set; }

        public UserChannel Channel { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

    }
}
