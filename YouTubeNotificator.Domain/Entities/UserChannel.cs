namespace YouTubeNotificator.Domain.Entities
{
    public class UserChannel
    {
        public Guid Id { get; set; }

        public string YoutubeName { get; set; }

        public string YoutubeId { get; set; }

        public string? PlaylistId { get; set; }

        public string YoutubeUrl { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<ChannelVideo> Videos { get; set; }
    }
}
