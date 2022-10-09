using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTubeNotificator.Domain.Entities;

namespace YouTubeNotificator.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property<Guid>(x => x.Id);
            builder.HasKey(x => x.Id);
            //builder.HasMany(x => x.Channels);

            builder.Property(x => x.TelegramId).IsRequired();
            builder.HasIndex(x => x.TelegramId).IsUnique();

            builder.ToTable("Users");
        }
    }
}
