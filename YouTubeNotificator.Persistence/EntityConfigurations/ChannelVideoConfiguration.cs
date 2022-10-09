using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Persistence.EntityConfigurations
{
    internal class ChannelVideoConfiguration : IEntityTypeConfiguration<ChannelVideo>
    {
        public void Configure(EntityTypeBuilder<ChannelVideo> builder)
        {
            builder.Property<Guid>(x => x.Id);
            builder.HasKey(x => x.Id);

            builder.Property<Guid>(x => x.ChannelId).IsRequired();
            builder.HasOne(x => x.Channel)
                .WithMany(x => x.Videos)
                .HasForeignKey(x => x.ChannelId);
            builder.HasIndex(x => new { x.Id, x.ChannelId }).IsUnique();

            builder.Property<DateTime>(x => x.Date).IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(500);
        }
    }
}
