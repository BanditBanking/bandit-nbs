using Bandit.NBS.MgdbRepository.Models;
using MongoDB.Driver;

namespace Bandit.NBS.MgdbRepository.Utils
{
    public class MgdbIndexer
    {

        public static async Task CreateIndexesAsync(IMongoDatabase database)
        {
            // Challenge Id Index
            var challengeDb = database.GetCollection<Challenge>("BD_OPER_PROC_AUTH");
            var options = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<Challenge>("ChallengeId");
            var indexDefinition = new IndexKeysDefinitionBuilder<Challenge>().Ascending(field);
            var indexModel = new CreateIndexModel<Challenge>(indexDefinition, options);
            challengeDb.Indexes.CreateOne(indexModel);
        }
    }
}
