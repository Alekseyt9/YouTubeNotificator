namespace YouTubeNotificator.Domain.Entities
{
    public class UserChannel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string YoutubeId { get; set; }

        public Guid UserId { get; set; }

        //public ICollection<ChannelVideo> Videos { get; set; }
    }
}
