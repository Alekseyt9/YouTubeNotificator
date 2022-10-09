
using System.Text;
using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices
{
    public class TelegramNotificationFormatter : INotificationFormatter
    {

        public string FormatMessage(NotificationData data)
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
