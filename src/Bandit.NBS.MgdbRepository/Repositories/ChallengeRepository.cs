using Bandit.NBS.MgdbRepository.Models;
using MongoDB.Driver;

namespace Bandit.NBS.MgdbRepository.Repositories
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly IMongoCollection<Challenge> _challenges;

        public ChallengeRepository(IMongoDatabase database)
        {
            _challenges = database.GetCollection<Challenge>("BD_OPER_PROC_AUTH");
        }

        public async Task AddChallengeAsync(Challenge challenge)
        {
            try
            {
                await _challenges.InsertOneAsync(challenge);
            } catch (Exception) { } // Volontary ignored, will fail if duplicate challenge
        }
    }
}
