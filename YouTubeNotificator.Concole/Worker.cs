
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YouTubeNotificator.Domain.Commands;
using YouTubeNotificator.Domain.Sevices;

namespace YouTubeNotificator.Concole
{
    internal class Worker : BackgroundService
    {
        private readonly INotificationTaskManager _taskManager;
        private readonly ITelegramBot _bot;
        private readonly ITelegramCommandFactory _factory;
        private readonly ITelegramCommandParser _parser;
        private readonly IMediator _mediator;
        private readonly ILogger<Worker> _logger;

        public Worker(
            INotificationTaskManager taskManager, ITelegramBot bot,
            ITelegramCommandFactory factory, ITelegramCommandParser parser,
            IMediator mediator, ILogger<Worker> logger)
        {
            _taskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
            _bot = bot ?? throw new ArgumentNullException();
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[!] Service running");
            try
            {
                await Start();
                _logger.LogInformation("[!] Start()");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("[!] " + ex.Message);
            }

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[!] Service stopped");
            
            await base.StopAsync(cancellationToken);
        }

        private async Task Start()
        {
            //var taskManager = host.Services.GetRequiredService<INotificationTaskManager>();
            await _taskManager.Start();

            //var bot = host.Services.GetRequiredService<ITelegramBot>();
            _bot.ReceiveMessage += async (sender, args) =>
            {
                //var factory = host.Services.GetRequiredService<ITelegramCommandFactory>();
                //var parser = host.Services.GetRequiredService<ITelegramCommandParser>();
                var cmdInfo = _parser.Parse(args.Message);
                if (cmdInfo == null)
                {
                    return;
                }

                var cmd = (TelegramCommandBase)_factory.Create(cmdInfo);
                cmd.Context = new TelegramBotContext()
                {
                    TelegramChannelId = args.ChannelId
                };
                //var mediator = host.Services.GetRequiredService<IMediator>();
                await _mediator.Send(cmd);
            };
        }

    }
}
