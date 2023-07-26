using Bandit.NBS.Daemon.Clients;
using Bandit.NBS.Daemon.Commands;
using Bandit.NBS.Daemon.Models.DTOs;
using Bandit.NBS.Daemon.Services;

namespace Bandit.NBS.Daemon.CommandHandlers
{
    public class TransactionsAnalyticsHandler : ICommandHandler
    {
        private readonly ILogger<TransactionsAnalyticsHandler> _logger;
        private readonly ITransactionService _transactionService;

        public static Predicate<ICommand> Predicate => command => command.Type == nameof(TransactionAnalyticsCommand);

        public TransactionsAnalyticsHandler(ILogger<TransactionsAnalyticsHandler> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        public async Task HandleAsync(SslClient client, ICommand command, CancellationToken cancellationToken)
        {
            var parsedCommand = (TransactionAnalyticsCommand) command;
            try
            {
                _logger.LogDebug("Received a transaction analytics command");
                await _transactionService.AddTransactionAsync(parsedCommand.Transaction);
                await client.SendAsync(new TransactionsAnalyticsResultDTO { IsSuccess = true });
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await client.SendAsync(new TransactionsAnalyticsResultDTO { IsSuccess = false });
            }
        }
    }
}
