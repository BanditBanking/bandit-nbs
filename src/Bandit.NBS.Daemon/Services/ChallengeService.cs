using Bandit.NBS.Daemon.Mappers;
using Bandit.NBS.Daemon.Models;
using Bandit.NBS.Daemon.Models.DTOs;
using Bandit.NBS.MgdbRepository.Repositories;

namespace Bandit.NBS.Daemon.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task AddChallengesAsync(List<AnalyticsChallenge> challenges) => challenges.ForEach(async challenge => await _challengeRepository.AddChallengeAsync(challenge.ToModel()));
    }
}
