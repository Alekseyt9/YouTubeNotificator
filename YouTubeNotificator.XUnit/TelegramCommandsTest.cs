
using Xunit;
using YouTubeNotificator.Domain.Commands;
using YouTubeNotificator.Domain.Sevices;
using YouTubeNotificator.Domain.Sevices.Impl;
using YouTubeNotificator.Domain.Sevices.Implementation;

namespace YouTubeNotificator.XUnit
{
    public class TelegramCommandsTest
    {
        [Fact]
        public void Test1()
        {
            ITelegramCommandParser parser = new TelegrammCommandParser();
            ITelegramCommandFactory factory = new TelegramCommandFactory();
            var cmdInfo = parser.Parse("/start");
            Assert.Equal(cmdInfo.Kind, TelegramCommandKind.Start);
            var cmd = (TelegramCommandBase)factory.Create(cmdInfo);
            cmd.Context = new TelegramBotContext() { TelegramChannelId = "" };
        }
    }
}
