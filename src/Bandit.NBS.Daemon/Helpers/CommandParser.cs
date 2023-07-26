using Bandit.NBS.Daemon.Commands;
using Bandit.NBS.Daemon.Exceptions;
using System.Text.Json;

namespace Bandit.NBS.Daemon.Helpers
{
    public class CommandParser : ICommandParser
    {

        public ICommand Parse(string rawCommand)
        {
            var commandType = JsonSerializer.Deserialize<RawCommand>(rawCommand)?.Type;
            ICommand? command = commandType switch
            {
                nameof(TransactionAnalyticsCommand) => JsonSerializer.Deserialize<TransactionAnalyticsCommand>(rawCommand),
                nameof(ChallengeAnalyticsCommand) => JsonSerializer.Deserialize<ChallengeAnalyticsCommand>(rawCommand),
                _ => throw new UnknownCommandException($"Command \"{rawCommand}\" not recognized")
            };
            return command;
        }
    }
}
