using Bandit.NBS.Daemon.Models;

namespace Bandit.NBS.Daemon.Commands
{
    public class ChallengeAnalyticsCommand : ICommand
    {
        public string Type { get; set; } = nameof(ChallengeAnalyticsCommand);
        public List<AnalyticsChallenge> Challenges { get; set; }
    }
}
