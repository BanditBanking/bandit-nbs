using Bandit.NBS.Daemon.Models;

namespace Bandit.NBS.Daemon.Mappers
{
    public static class ChallengeMapper
    {
        public static MgdbRepository.Models.Challenge ToModel(this AnalyticsChallenge challenge) => new()
        {
            ChallengeId = challenge.ChallengeId,
            ChallengeType = challenge.ChallengeType,
            BankId = challenge.BankId,
            ClientId = challenge.ClientId,
            BirtDate = challenge.BirtDate,
            Age = challenge.Age,
            Gender = challenge.Gender,
            RequestTime = challenge.RequestTime,
            AttemptCount = challenge.AttemptCount,
            ResponseTime = challenge.ResponseTime,
            Decision = challenge.Decision,
            MaxAttemptsReached = challenge.MaxAttemptsReached,
            DecisionTime = challenge.DecisionTime
        };
    }
}
