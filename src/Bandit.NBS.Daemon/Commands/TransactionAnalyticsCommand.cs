using Bandit.NBS.Daemon.Models;

namespace Bandit.NBS.Daemon.Commands
{
    public class TransactionAnalyticsCommand : ICommand
    {
        public string Type { get; set; } = nameof(TransactionAnalyticsCommand);
        public AnalyticsTransaction Transaction { get; set; }
    }
}
