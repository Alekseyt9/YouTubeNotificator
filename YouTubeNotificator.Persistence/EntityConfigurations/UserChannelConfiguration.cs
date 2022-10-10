using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Persistence.EntityConfigurations
{
    internal class UserChannelConfiguration : IEntityTypeConfiguration<UserChannel>
    {
        public void Configure(EntityTypeBuilder<UserChannel> builder)
        {
            builder.Property<Guid>(x => x.Id);
            builder.HasKey(x => new { x.Id });

            builder.Property<Guid>(x => x.UserId).IsRequired();
            builder.HasIndex(x => new { x.Id, x.UserId }).IsUnique();

            builder.Property(x => x.YoutubeId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.YoutubeName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.YoutubeUrl).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PlaylistId).HasMaxLength(100);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Channels)
                .HasForeignKey(x => x.UserId);
        }

    }
}
