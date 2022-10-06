using YouTubeNotificator.Domain.Model;

namespace YouTubeNotificator.Domain.Sevices.Impl
{
    public class TelegramCommandParser : ITelegramCommandParser
    {
        public CommandInfo Parse(string message)
        {
            var res = new CommandInfo();

            var arr = message.Split(" ")
                .Where(x => !string.IsNullOrEmpty(x.Trim(' '))).ToArray();

            string cmd;
            if (arr.Length > 0)
            {
                cmd = arr[0];
                res.Kind = GetCommandKind(cmd);
                if (res.Kind == TelegramCommandKind.NotCommand)
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException("Cmd missed");
            }

            if (arr.Length > 1)
            {
                for (var i = 1; i < arr.Length; i++)
                {
                    res.Params.Add(arr[i]); 
                }
            }

            return res;
        }

        private TelegramCommandKind GetCommandKind(string cmdStr)
        {
            switch (cmdStr)
            {
                case "/ls": return TelegramCommandKind.List;
                case "/start": return TelegramCommandKind.Start;
                case "/del": return TelegramCommandKind.Remove;
                case "/add": return TelegramCommandKind.Add;
                default: return TelegramCommandKind.NotCommand;
            }
        }

    }
}
