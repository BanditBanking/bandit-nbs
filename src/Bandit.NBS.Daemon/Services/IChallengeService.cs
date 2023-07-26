using Bandit.NBS.Daemon.Models;
using Bandit.NBS.Daemon.Models.DTOs;

namespace Bandit.NBS.Daemon.Services
{
    public interface IChallengeService
    {
        Task AddChallengesAsync(List<AnalyticsChallenge> challenges);
    }
}
