
using System.Text;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public class TelegramNotificator : INotificator
    {
        private ITelegramBot _bot;

        public TelegramNotificator(ITelegramBot bot)
        {
            _bot = bot;
        }

        public void SendNotification(NotificationData data)
        {
            _bot.SendMessage(
                data.TelegramBotContext.TelegramChannelId,
                FormatMessage(data)
            );
        }

        private string FormatMessage(NotificationData data)
        {
            var sb = new StringBuilder();

            foreach (var tuple in data.Data)
            {
                var chan = tuple.Item1;
                sb.AppendLine($"<b>{chan.YoutubeName}</b>");

                foreach (var vid in tuple.Item2)
                {
                    sb.AppendLine($"    • [{vid.Date.ToShortTimeString()} {vid.Date.ToShortDateString()}] <a href='{vid.Url}'>{vid.Name}</a>");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
