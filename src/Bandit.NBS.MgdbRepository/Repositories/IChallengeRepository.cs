using Bandit.NBS.MgdbRepository.Models;

namespace Bandit.NBS.MgdbRepository.Repositories
{
    public interface IChallengeRepository
    {
        Task AddChallengeAsync(Challenge challenge);
    }
}
