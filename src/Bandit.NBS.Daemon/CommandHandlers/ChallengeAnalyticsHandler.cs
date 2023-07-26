using Bandit.NBS.Daemon.Clients;
using Bandit.NBS.Daemon.Commands;
using Bandit.NBS.Daemon.Models.DTOs;
using Bandit.NBS.Daemon.Services;
using Microsoft.Extensions.Logging;

namespace Bandit.NBS.Daemon.CommandHandlers
{
    public class ChallengeAnalyticsHandler : ICommandHandler
    {
        private readonly ILogger<ChallengeAnalyticsHandler> _logger;
        private readonly IChallengeService _challengeService;

        public static Predicate<ICommand> Predicate => command => command.Type == nameof(ChallengeAnalyticsCommand);

        public ChallengeAnalyticsHandler(ILogger<ChallengeAnalyticsHandler> logger, IChallengeService challengeService)
        {
            _logger = logger;
            _challengeService = challengeService;
        }

        public async Task HandleAsync(SslClient client, ICommand command, CancellationToken cancellationToken)
        {
            var parsedCommand = (ChallengeAnalyticsCommand) command;
            try
            {
                _logger.LogDebug("Received a challenge analytics command");
                await _challengeService.AddChallengesAsync(parsedCommand.Challenges);
                await client.SendAsync(new ChallengeAnalyticsResultDTO { IsSuccess = true });
            } catch (Exception)
            {
                _logger.LogError("An exception occured while trying to process a challenge analytics command");
                await client.SendAsync(new ChallengeAnalyticsResultDTO { IsSuccess = false });
            }
        }
    }
}
